using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IEnemyController
{
    [Header("Basic Enemy Attributes")]
    [SerializeField] private float maxHealth = 1f;
    [SerializeField] private float deathDecayTime = 3f;

    [SerializeField] private ClearoutStage ClearoutStage;
    [SerializeField] private Collider enemyCollider;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void HitByPlayer()
    {
        currentHealth--;
        if(currentHealth <= 0 )
        {
            EnemyDeath();
        } 
        else
        {
            //do normal hit stuff
            //flash red
        }
    }

    public virtual void EnemyDeath()
    {
        gameObject.tag = "Dead";
        ClearoutStage.EnemyReportingDefeat(this.gameObject);
        //enemyCollider.enabled = false;
        Destroy(this.gameObject, deathDecayTime);
    }
}
