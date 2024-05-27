using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveStage : StageManager
{
    public override void TimerHasRanOut()
    {
        StageWasCleared();
    }
}
