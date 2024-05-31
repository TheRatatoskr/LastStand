using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveStage : StageManager
{
    [SerializeField] private List<GameObject> hazards;
    [SerializeField] private bool silenceObjects = true;

    public override void TimerHasRanOut()
    {
        Debug.Log("stage was cleared");
        StageWasCleared();
    }

    public override void StageWasCleared()
    {
        SilenceTheLasers();
        base.StageWasCleared();
    }

    public override void StageWasFailed()
    {
        SilenceTheLasers();
        base.StageWasFailed();
    }

    private void SilenceTheLasers()
    {
        if(silenceObjects)
        {
            foreach (GameObject hazard in hazards)
            {
                AudioSource sound = hazard.GetComponent<AudioSource>();
                if (sound != null) sound.Stop();
            }
        }
    }
}
