
using System;
using UnityEngine;

namespace BoadaTowerDefense
{
    public static class TDFibonacci
    {
        public static int EnemiesForWave(int wave)
        {
            if (wave < 1 || wave > 30)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(wave),
                    "Wave must be between 1 and 30."
                );
            }

            int previous = 0;
            int current = 1;

            checked
            {
                for (int i = 0; i < wave + 2; i++)
                {
                    int next = previous + current;

                    previous = current;
                    current = next;
                }
            }

            return previous;
        }

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(
            RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void TestFibonacci()
        {
            try
            {
                for (int wave = 1; wave <= 6; wave++)
                {
                    int enemies = EnemiesForWave(wave);

                    Debug.Log(
                        "Wave " + wave +
                        " - Enemies: " + enemies
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
#endif
    }
}
