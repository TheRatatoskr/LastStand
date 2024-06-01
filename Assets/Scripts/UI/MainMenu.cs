using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        if (Time.timeScale < 1f)
        {
            Time.timeScale = 1f;
        }

        gameManager = FindObjectOfType<GameManager>();
    }

    public void NewGameClicked()
    {
        gameManager = FindObjectOfType<GameManager>();
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
