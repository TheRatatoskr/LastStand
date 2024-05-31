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
    [SerializeField] private Transform wallRaySourceFeet;
    [SerializeField] private Transform wallRaySourceBody;
    [SerializeField] private Transform wallRaySourceHead;

    private bool footHitWall;
    private bool bodyHitWall;
    private bool headHitWall;

    [SerializeField] private float wallCheckLength;
    [SerializeField] private float stopIfWallIsThisClose = .13f;

    private int isLeft = 1;

    private Vector2 currentMovement;

    private float wallAdjustment = 1;
    private bool isAlive = true;

    private void Start()
    {
        inputReader.MovementKeysChanged += CalculateMovement;
    }
    private void Update()
    {
        WallCheckerFoot();
        WallCheckerBody();
        WallCheckerHead();

        if (headHitWall || footHitWall || bodyHitWall)
        {
            wallAdjustment = 0;
        }
        else
        {
            wallAdjustment = 1;
        }
        
        if (Time.timeScale <= .9f) return; 
        transform.Translate(currentMovement.x * moveSpeed * Time.deltaTime * wallAdjustment, 0, 0);
        animator.SetFloat("forwardSpeed", Math.Abs(currentMovement.x));

        //position validation checks
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.rotation = Quaternion.identity;

    }

    private void WallCheckerFoot()
    {
        Ray ray = new Ray(wallRaySourceFeet.position, Vector3.right * isLeft);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, wallCheckLength))
        {
            if(hitInfo.distance <= stopIfWallIsThisClose)
            {
                Debug.Log("I hit the wall");
                footHitWall = true;
            }
            else
            {
                Debug.Log("No walls here!");
                footHitWall = false;
            }
        }
        else
        {
            Debug.Log("nothing found");
            footHitWall = false;
        }

        Debug.DrawRay(ray.origin, ray.direction * wallCheckLength, Color.magenta);

    }

    private void WallCheckerBody()
    {
        Ray ray = new Ray(wallRaySourceBody.position, Vector3.right * isLeft);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, wallCheckLength))
        {
            if (hitInfo.distance <= stopIfWallIsThisClose)
            {
                Debug.Log("I hit the wall");
                bodyHitWall = true;
            }
            else
            {
                Debug.Log("No walls here!");
                bodyHitWall= false;
            }

        }
        else
        {
            Debug.Log("nothing found");
            bodyHitWall = false;
        }

        Debug.DrawRay(ray.origin, ray.direction * wallCheckLength, Color.magenta);

    }

    private void WallCheckerHead()
    {
        Ray ray = new Ray(wallRaySourceHead.position, Vector3.right * isLeft);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, wallCheckLength))
        {
            if (hitInfo.distance <= stopIfWallIsThisClose)
            {
                Debug.Log("I hit the wall");
                headHitWall = true;
            }
            else
            {
                Debug.Log("No walls here!");
                headHitWall= false;
            }

        }
        else
        {
            Debug.Log("nothing found");
            headHitWall = false;
        }

        Debug.DrawRay(ray.origin, ray.direction * wallCheckLength, Color.magenta);

    }

    private void CalculateMovement(Vector2 joyStick)
    {

        currentMovement = joyStick;
        if(currentMovement.x > 0)
        {
            isLeft = 1;
            model.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90f, 0);
        } 
        else if (currentMovement.x < 0)
        {
            isLeft = -1;
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
