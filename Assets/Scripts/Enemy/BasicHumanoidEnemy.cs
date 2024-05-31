using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHumanoidEnemy : BasicEnemy
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float backForceOnDeath = 300f;
    [SerializeField] private float upForceOnDeath = 200f;

    [Header("Waypoints")]

    [SerializeField] private Transform leftWaypoint;
    [SerializeField] private Transform rightWaypoint;

    [Header("Ragdoll Parts")]
    [SerializeField] private List<Collider> ragdollColliders;
    [SerializeField] private List<Rigidbody> ragdollBodies;

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;

    [Header("Debug Only")]
    [SerializeField] private bool isWalkingLeft = true;

    private bool isAlive = true;

    
    private void Update()
    {
        if(isAlive)
        {
            HandlePacing();
            PositionValidationChecks();
        }
    }

    private void PositionValidationChecks()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (isWalkingLeft)
        {
            transform.eulerAngles = new Vector3(0, 270f, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90f, 0);
        }
    }

    private void HandlePacing()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if(transform.position.x <= leftWaypoint.position.x)
        {
            isWalkingLeft = false;
        }

        if (transform.position.x >= rightWaypoint.position.x)
        {
            isWalkingLeft = true;
        }

    }
    public override void EnemyDeath()
    {
        base.EnemyDeath();
        isAlive = false;
        moveSpeed = 0;
        anim.enabled = false;
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.Play();
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }
        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = false;
        }
    }
}
