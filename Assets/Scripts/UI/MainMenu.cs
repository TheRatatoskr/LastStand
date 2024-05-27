using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public void CreditsClicked()
    {
        Debug.Log("Credits Clicked");
        SceneManager.LoadScene("Credits");
    }

    public void ExitGameClicked()
    {
        #if UNITY_EDITOR 
        if(EditorApplication.isPlaying) 
        { 
            EditorApplication.isPlaying = false; 
        } 
        #endif

        Application.Quit();
    }
}
