using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearoutStage : StageManager
{
    [Header("Clearout Settings")]
    [SerializeField] List<GameObject> enemies;
    
    public void EnemyReportingDefeat(GameObject enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            StageWasCleared();
        }
    }
}
