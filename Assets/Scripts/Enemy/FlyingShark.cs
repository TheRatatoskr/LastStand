using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShark : BasicEnemy
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float backForceOnDeath = 300f;
    [SerializeField] private float upForceOnDeath = 200f;

    [SerializeField] private bool lookingShark;

    [Header("Waypoints")]

    [SerializeField] private Transform leftWaypoint;
    [SerializeField] private Transform rightWaypoint;


    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator anim;

    [Header("Debug Only")]
    [SerializeField] private bool isWalkingLeft = true;

    private float rotateLeftAngle = 270f;
    private float rotateRightAngle = 90f;



    private bool isAlive = true;

    private void Update()
    {
        if (isAlive)
        {
            HandlePacing();
            PositionValidationChecks();
        }
    }

    private void PositionValidationChecks()
    {
        //keep shark on correct z coord to hit player
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            if (isWalkingLeft)
            {
                transform.eulerAngles = new Vector3(0, rotateLeftAngle, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, rotateRightAngle, 0);
            }

    }

    private void HandlePacing()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if (transform.position.x <= leftWaypoint.position.x)
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
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        isAlive = false;
        moveSpeed = 0;
        anim.enabled = false;

    }
}
