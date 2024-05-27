using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void NewGameClicked()
    {
        gameManager.StartTheGame();
    }

    public void SettingsClicked()
    {
        Debug.Log("Settings Clicked");
        SceneManager.LoadScene("Settings");
    }

    public void CreditsClicked()
    {
        Debug.Log("Credits Clicked");
        SceneManager.LoadScene("Credits");
    }

    public void ExitGameClicked()
    {
        Debug.Log("Exit Clicked");
        Application.Quit();
    }
}
