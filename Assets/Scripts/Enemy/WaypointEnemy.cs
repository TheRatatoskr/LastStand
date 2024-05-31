using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointEnemy : BasicEnemy
{
    [Header("Required Waypoint Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject enemyObject;

    [Header("Creature Sounds")]
    [SerializeField] private AudioClip creatureDeath;
    [SerializeField] private AudioClip creatureNoiseClip;

    [Header("Sound Settings")]
    [SerializeField] private float noisePitchRange;
    [SerializeField] private float timeBetweenNoises;
    [SerializeField] private float noiseTimeAdjustment;

    [Header("Waypoint Move Settings")]
    [SerializeField] private float wayPointDistanceCheck = 1f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject wayPointContainer;
    [SerializeField] private List<Transform> waypointList;

    private int currentWaypointIndex;

    private int nextIndexAdjuster = 1;

    private bool isAlive = true;

    private void Start()
    {
        foreach(Transform child in wayPointContainer.transform)
        {
            waypointList.Add(child);
        }
        currentWaypointIndex = 0;
        StartCoroutine(SoundSequencer());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointList[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position , waypointList[currentWaypointIndex].position) < wayPointDistanceCheck)
        {
            if (currentWaypointIndex + nextIndexAdjuster < 0 || currentWaypointIndex + nextIndexAdjuster >= waypointList.Count)
            {
                nextIndexAdjuster = nextIndexAdjuster * -1;
            }

            currentWaypointIndex+= nextIndexAdjuster;
        }

        if (waypointList[currentWaypointIndex].position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
    }

    #region Sound Fiddly Bits
    private IEnumerator SoundSequencer()
    {
        yield return new WaitForSeconds(Random.Range(timeBetweenNoises - noiseTimeAdjustment, timeBetweenNoises + noiseTimeAdjustment));
        while (isAlive)
        {
            PlayCreatureNoise(creatureNoiseClip);
            yield return new WaitForSeconds(Random.Range(timeBetweenNoises - noiseTimeAdjustment, timeBetweenNoises + noiseTimeAdjustment));
        }
    }

    private void PlayCreatureNoise(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(1f - noisePitchRange, 1f + noisePitchRange);
        audioSource.Play();
    }
    #endregion

    public override void EnemyDeath()
    {
        isAlive = false;
        base.EnemyDeath();
        PlayCreatureNoise(creatureDeath);
        rb.useGravity = true;
        rb.isKinematic = false;
        isAlive = false;
        anim.enabled = false;
    }
}
