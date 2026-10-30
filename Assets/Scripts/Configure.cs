using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Configure : MonoBehaviour
{
    [SerializeField] private  Button buttonMenu;

    private void Awake()
    {
        buttonMenu.onClick.AddListener(ButtonMenu);
    }

    private void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
