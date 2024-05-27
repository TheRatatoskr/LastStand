using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDeath : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private List<Rigidbody> ragdollBody;
    [SerializeField] private List<Collider> ragdollColliders;

    [Header("Components")]
    [SerializeField] private Collider groundCollider;
    [SerializeField] private Collider airCollider;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    [SerializeField] private ActionMovement actionMovement;
    [SerializeField] private ActionAttack actionAttack;
    [SerializeField] private ActionJumping actionJumping;

    private StageManager stageManager;

    private bool isDead = false;

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") OnPlayerDeath();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hazard") OnPlayerDeath();
    }
    private void OnPlayerDeath()
    {
        if (isDead) return;
        isDead = true;

        animator.enabled = false;
        groundCollider.enabled = false;
        airCollider.enabled = false;

        actionMovement.PlayerIsDead();
        actionAttack.PlayerIsDead();
        actionJumping.PlayerIsDead();

        foreach (Collider col in ragdollColliders)
        { 
            col.enabled = true; 
        }

        foreach (Rigidbody body in ragdollBody)
        {
            body.isKinematic = false;
        }
        
        audioSource.clip = deathSound;
        audioSource.Play();

        stageManager.StageWasFailed();
    }
}
