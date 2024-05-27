using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [Header("Timeline Canvas Objects")]
    [SerializeField] private TMP_Text stageTypeText;
    [SerializeField] private TMP_Text finalScoreValue;

    [Header("Timelines")]
    [SerializeField] private PlayableDirector introLine;
    [SerializeField] private PlayableDirector completedLine;
    [SerializeField] private PlayableDirector deathline;
    [SerializeField] private PlayableDirector gameOverLine;


    private string stageType;

    private StageManager stageController;

    private bool isStarted = false;

    private void Start()
    {
        stageController = FindObjectOfType<StageManager>();
        stageType = stageController.GetStageType(this);
        stageTypeText.text = stageType;
        Time.timeScale = 0f;
        
    }

    private void LateUpdate()
    {
        if (!isStarted)
        {
            introLine.Play();
            isStarted = true;
        }
        
    }

    #region StageManagerSignals
    public void StageHasBeenCompleted()
    {
        completedLine.Play();
    }
    
    public void OnPlayerDeath()
    {
        deathline.Play();
    }

    public void OnPlayerGameOver(int currentScore)
    {
        finalScoreValue.text = currentScore.ToString();
        gameOverLine.Play();
    }

    #endregion

    #region SignalRecievers
    public void IntroTimelineHasFinished()
    {
        Time.timeScale = 1f;
    }

    public void CompletedStageTimelineHasFinished()
    {
        stageController.CompletedStageTimelineIsDone();
    }

    public void PlayerDeathTimelineHasFinished()
    {
        stageController.DeathTimelineFinished();
    }

    public void FreezeTimeScale()
    {
        Time.timeScale = 0;
    }
    #endregion
}
