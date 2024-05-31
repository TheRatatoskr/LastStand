using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserFrog : BasicEnemy
{
    [Header("Required Frog Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;

    [Header("Frog Ribbit Sounds")]
    [SerializeField] private AudioClip ribbitDeath;
    [SerializeField] private AudioClip ribbitClip;

    [Header("Frog Sound Settings")]
    [SerializeField] private float ribbitPitchRange;
    [SerializeField] private float timeBetweenRibbit;
    [SerializeField] private float ribbitTimerAdjustment;

    [Header("Frog Jump Settings")]
    [SerializeField] private float upForce;
    [SerializeField] private float forwardForce;

    [SerializeField] private float jumpWaitTime;
    [SerializeField] private float jumpTimerAdjustment;

    private bool isAlive = true;


    private void Start()
    {
        StartCoroutine(RibbitSequencer());
        StartCoroutine(JumpSequencer());
    }

    private IEnumerator JumpSequencer()
    {
        yield return new WaitForSeconds(Random.Range(jumpWaitTime - jumpTimerAdjustment, jumpWaitTime + jumpTimerAdjustment));
        DoTheRibbitJump();
    }


    private IEnumerator RibbitSequencer()
    {
        yield return new WaitForSeconds(Random.Range(timeBetweenRibbit - ribbitTimerAdjustment, timeBetweenRibbit + ribbitTimerAdjustment));

        while (isAlive)
        {
            PlayRibbitNoise(ribbitClip);
            yield return new WaitForSeconds(Random.Range(timeBetweenRibbit - ribbitTimerAdjustment, timeBetweenRibbit + ribbitTimerAdjustment));
        }
    }

    private void PlayRibbitNoise(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(1f- ribbitPitchRange, 1f + ribbitPitchRange);
        audioSource.Play();
    }


    private void DoTheRibbitJump()
    {
        if(Random.Range(0,2) == 0)
        {
            transform.eulerAngles = new Vector3(0, 270f, 0);
            rb.AddForce(Vector3.up * upForce + Vector3.left * forwardForce);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90f, 0);
            rb.AddForce(Vector3.up * upForce + Vector3.right * forwardForce);
        }

        anim.SetTrigger("jump");

    }



    public override void EnemyDeath()
    {
        isAlive = false;
        base.EnemyDeath();
        PlayRibbitNoise(ribbitDeath);
        rb.useGravity = true;
        rb.isKinematic = false;
        isAlive = false;
        anim.enabled = false;
    }
}
