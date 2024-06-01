using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ActionJumping : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpLength;

    [SerializeField] private float jumpSpeed;
    private float activeJumpSpeed = 0;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float upMass;
    [SerializeField] private float downMass;
    [SerializeField] private float jumpToAirChangeSpeed = 1f;
    [SerializeField] private float landRayCheckLength;
    [SerializeField] private float floorCheckerLength = .4f;



    [Header("Required Components")]
    [SerializeField] private InputReader inputReader;

    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Collider onFloorCollider;
    [SerializeField] private Collider inAirCollider;

    [SerializeField] private Transform landRaySource;


    [Header("Debug Only Plox")]
    [SerializeField] private bool isOnTheFloor = true;
    [SerializeField] private float distanceFromFloor;

    private bool isAirborne = false;

    private bool isAlive = true;

    private void Start()
    {
        inputReader.JumpButtonPressed += JumpFromGround;
    }

    private void Update()
    {
        Ray ray = new Ray(landRaySource.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, landRayCheckLength))
        {
            Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.magenta);
            if (hitInfo.collider.gameObject.tag == "Floor")
            {
                distanceFromFloor = hitInfo.distance;
                LandingColliderChanger();
            }
            
            if (distanceFromFloor > floorCheckerLength)
            {
                isOnTheFloor = false;
                animator.SetBool("inAir", true);
                animator.SetBool("hitGround", false);
            }
            else
            {
                isOnTheFloor = true;
                animator.SetBool("inAir", false);
                animator.SetBool("hitGround", true);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * landRayCheckLength, Color.magenta);
        }

        transform.Translate(Vector3.up * activeJumpSpeed * Time.deltaTime);

    }

    private void JumpFromGround(bool buttonState)
    {
        if (isAlive && Time.timeScale >= .9f)
        {
            if (isOnTheFloor && buttonState)
            {
                activeJumpSpeed = jumpSpeed;
                rb.useGravity = false;
                StartCoroutine(JumpWaitForSecondsToFall());
                animator.SetBool("inAir", true);
                JumpingColliderChange();
                animator.SetBool("hitGround", false);
                audioSource.clip = jumpSound;
                audioSource.Play();
            }
            else if (!isOnTheFloor && !buttonState)
            {
                activeJumpSpeed = 0;
                rb.useGravity = true;
            }
        }
    }

    private IEnumerator JumpWaitForSecondsToFall()
    {
        yield return new WaitForSeconds(jumpLength);
        rb.useGravity = true;
        activeJumpSpeed = 0;
    }
    private void JumpingColliderChange()
    {
        onFloorCollider.enabled = false;
        inAirCollider.enabled = true;
        isAirborne = true;
        //yield return new WaitForSeconds(jumpLength - jumpToAirChangeSpeed);
        //rb.useGravity = true;
    }

    private void LandingColliderChanger()
    {
        onFloorCollider.enabled = true;
        inAirCollider.enabled = false;
    }

    public void PlayerIsDead()
    {
        rb.useGravity = true;
        activeJumpSpeed = 0;
        inputReader.JumpButtonPressed -= JumpFromGround;
    }

    private void OnDestroy()
    {
        inputReader.JumpButtonPressed -= JumpFromGround;
    }
}
