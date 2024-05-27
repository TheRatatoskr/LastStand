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

    private bool isAlive = true;

    private void Update()
    {

            HandlePacing();
        
        
        //if (transform.position.y < 0) transform.position = new Vector3(transform.position.x,0,0);
    }

    private void HandlePacing()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if(transform.position.x <= leftWaypoint.position.x)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + -90f, 0);
        }

        if (transform.position.x >= rightWaypoint.position.x)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90f, 0);
        }

    }
    public override void EnemyDeath()
    {
        base.EnemyDeath();
        moveSpeed = 0;
        anim.enabled = false;
        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }
        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = false;
            //body.AddForce(-Vector3.forward * backForceOnDeath + Vector3.up * upForceOnDeath);
        }
        //_rigidbody.AddForce(-Vector3.forward * backForceOnDeath + Vector3.up * upForceOnDeath);
    }
}
