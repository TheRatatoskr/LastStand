using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class ActionMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject model;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 currentMovement;

    private bool isAlive = true;

    private void Start()
    {
        inputReader.MovementKeysChanged += CalculateMovement;
    }
    private void Update()
    {
        transform.Translate(currentMovement.x * moveSpeed * Time.deltaTime, 0, 0);
        animator.SetFloat("forwardSpeed", Math.Abs(currentMovement.x));

        //position validation checks
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.rotation = Quaternion.identity;

    }
    private void CalculateMovement(Vector2 joyStick)
    {

        currentMovement = joyStick;
        if(currentMovement.x > 0)
        {
            //sleepy joe says better way is totes possible
            model.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90f, 0);
        } 
        else if (currentMovement.x < 0)
        {
            //sleepy joe says better way is totes possible
            model.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + -90f, 0);
        }
    }

    public void PlayerIsDead()
    {
        inputReader.MovementKeysChanged -= CalculateMovement;
        currentMovement = Vector2.zero;
    }

    private void OnDestroy()
    {
        inputReader.MovementKeysChanged -= CalculateMovement;
    }
}
