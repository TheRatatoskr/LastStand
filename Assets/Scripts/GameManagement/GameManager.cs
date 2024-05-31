using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameCanvas gameCanvas;

    [SerializeField] private List<string> actionScenes;

    [SerializeField] private int startingLifeCounter = 50;

    [SerializeField] private int numberOfCachedLevels = 5;
    [Header("DebugOnly")]

    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;

    [SerializeField] private int currentLives = 5;

    [SerializeField] private List<string> pickedLevels;

    private List<string> availableLevels;

    [SerializeField] private AudioClip titleScreenMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip gOverMusic;

    private AudioSource audioSource;

    #region Singleton
    public static GameManager instance = null;
    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        // If instance already exists and it's not this, destroy this
        // This enforces the singleton pattern, meaning there can only ever be one instance of a GameManager.
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start()
    {
        pickedLevels = new List<string>();

        for(int i = 0; i < numberOfCachedLevels; i++)
        {
            pickedLevels.Add(actionScenes[Random.Range(0, actionScenes.Count)]);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = titleScreenMusic;
        audioSource.Play();

    }

    //only used by new game button
    public void StartTheGame()
    {
        currentLives = startingLifeCounter;
        Debug.Log(currentLives.ToString());
        UpdateScore(0);
        //play sound maybe?
        LoadAnActionScene();
        audioSource.Stop();
        audioSource.clip = gameMusic;
        audioSource.Play();

    }

    //used by stage managers to report back stage successes
    public void StageWasCleared()
    {
        UpdateScore(1);
        LoadAnActionScene();
    }

    //stage manager requests a reload on failure
    public void ReloadFailedStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //stage manager want score
    public int GetScore()
    {
        return currentScore;
    }

    //returns a bool telling stage manager if we are GG
    public bool StageWasFailed()
    {
        currentLives--;
        if(currentLives <= 0)
        {
            audioSource.Stop();
            audioSource.clip = gOverMusic;
            audioSource.Play();
            return true;

        }
        return false;
    }

    #region InternalFunctions
    //internally to process all reported score changes
    private void UpdateScore(int scoreChange)
    {
        if(scoreChange == 0)
        {
            currentScore = 0;
            gameCanvas.UpdateScore(0);
        }
        else
        {
            currentScore += scoreChange;
            gameCanvas.UpdateScore(currentScore);
            if (currentScore > highScore)
            {
                highScore = currentScore;
                gameCanvas.UpdateHighScore(highScore);
            }
        }
    }
    
    //internal load next scene logic
    private void LoadAnActionScene()
    {

        string nextLevel = pickedLevels[0];
        pickedLevels.Remove(nextLevel);
        if(pickedLevels.Count < numberOfCachedLevels)
        {
            availableLevels = actionScenes.Except(pickedLevels).ToList();
            pickedLevels.Add(availableLevels[Random.Range(0, availableLevels.Count)]);
        }

        SceneManager.LoadScene(nextLevel);
    }
    #endregion
}
