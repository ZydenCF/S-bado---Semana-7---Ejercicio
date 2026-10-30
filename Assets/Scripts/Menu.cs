using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button configureButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        startButton.onClick.AddListener(StartButton);
        configureButton.onClick.AddListener(ConfigureButton);
    }

    private void StartButton()
    {
        SceneManager.LoadScene("Level");
    }

    private void ConfigureButton()
    {
        SceneManager.LoadScene("Configure");
    }

}
