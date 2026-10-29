using System.Collections.Generic;
using UnityEngine;

    public class TDPath : MonoBehaviour
    {
        public List<Transform> waypoints = new List<Transform>();

        private void Start()
        {
            if (waypoints.Count < 2 || waypoints.Contains(null))
            {
                Debug.LogError("El camino no esta configurado.");
                return;
            }

            Debug.Log("Camino listo: " + waypoints.Count + " puntos.");
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (waypoints[i] == null)
                    continue;

                if (i == 0)
                    Gizmos.color = Color.cyan;
                else if (i == waypoints.Count - 1)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.yellow;

                Gizmos.DrawSphere(waypoints[i].position, 0.25f);

                if (i < waypoints.Count - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.color = Color.yellow;

                    Gizmos.DrawLine(
                        waypoints[i].position,
                        waypoints[i + 1].position
                    );
                }
            }
        }
    }
