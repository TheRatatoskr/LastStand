using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class StageManager : MonoBehaviour
{
    [Header("Stage Manager Settings")]
    [SerializeField] private string stageType;
    [SerializeField] private float finishStageTimeScale = .5f;

    [SerializeField] private float stageTimeLimit = 0f;

    private float currentTimeSinceStart;
    private bool timerIsActive;


    [Header("Stage Manager Required Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera dramaticZoomCam;
    private TimelineController timelineController;

    [Header("Stage Manager Debug Only")]
    [SerializeField] private GameManager gameManager;



    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("AHHHHH SCREAM IN TERROR THE GAME MANAGER IS MISSING!");
        }

        if (stageTimeLimit > 0f)
        {
            timerIsActive = true;
        }

    }

    private void Update()
    {
        if (timerIsActive)
        {
            currentTimeSinceStart += Time.deltaTime;
            if (currentTimeSinceStart > stageTimeLimit)
            {
                StageWasFailed();
                timerIsActive = false;
            }
        }
    }

    //called when timer runs out. default is fail teh stage
    public virtual void TimerHasRanOut()
    {
        StageWasFailed();
    }

    //this is called from player when we die
    public void StageWasFailed()
    {
        ActivateSlowZoomCam();

        bool gameOver = gameManager.StageWasFailed();
        if (gameOver)
        {
            Debug.Log("Super Game Over");
            timelineController.OnPlayerGameOver(gameManager.GetScore());
        }
        else
        {
            Debug.Log("Player is dead but not over");
            timelineController.OnPlayerDeath();
        }

    }

    #region SendTimelineFunctions
    public virtual void StageWasCleared()
    {
        timerIsActive = false;
        //called from Stage type script when its specified criteria is met

        ActivateSlowZoomCam();

        timelineController.StageHasBeenCompleted();
    }


    #endregion

    #region FromTimelineFunctions
    public string GetStageType(TimelineController timeController)
    {
        timelineController = timeController;

        if (stageType == null) return "NOT SET";
        return stageType;
    }

    public void CompletedStageTimelineIsDone()
    {
        //stage manager will perform any cleanup that needs to happen and tell game manager its done 
        gameManager.StageWasCleared();
    }

    public void DeathTimelineFinished()
    {
        gameManager.ReloadFailedStage();
    }
    #endregion

    #region InternalReuseFunctions
    private void ActivateSlowZoomCam()
    {
        Time.timeScale = finishStageTimeScale;
        dramaticZoomCam.Priority = 20;
    }
    #endregion

}
