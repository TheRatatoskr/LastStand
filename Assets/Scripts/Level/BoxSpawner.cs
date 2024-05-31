using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    [SerializeField] private GameObject smallSpawnPointHolder;
    [SerializeField] private GameObject largeSpawnPointHolder;

    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float timerAdjuster;

    [SerializeField] private GameObject bigCratePrefab;
    [SerializeField] private GameObject smallCratePrefab;

    private bool isRunning = true;

    private List<Transform> bigSpawnPoints = new List<Transform>();

    private List<Transform> smallSpawnPoints= new List<Transform>();

    private void Start()
    {
        foreach (Transform child in smallSpawnPointHolder.transform)
        {
            smallSpawnPoints.Add(child);
        }

        foreach (Transform child in largeSpawnPointHolder.transform)
        {
            bigSpawnPoints.Add(child);
            smallSpawnPoints.Add(child);
        }

        StartCoroutine(SpawnSequencer());
    }

    private IEnumerator SpawnSequencer()
    {
        yield return new WaitForSeconds(Random.Range(timeBetweenSpawns-timerAdjuster, timeBetweenSpawns+timerAdjuster));

        while (isRunning)
        {
            PickCrate();
            yield return new WaitForSeconds(Random.Range(timeBetweenSpawns - timerAdjuster, timeBetweenSpawns + timerAdjuster));
        }
    }

    private void PickCrate()
    {
        if(Random.Range(0,2) == 0)
        {
            SpawnSmallCrate();
        }
        else
        {
            SpawnLargeCrate();
        }

    }

    private void SpawnSmallCrate()
    {
        Transform spawnPoint = smallSpawnPoints[Random.Range(0,smallSpawnPoints.Count)];
        Instantiate(smallCratePrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void SpawnLargeCrate()
    {
        Transform spawnPoint = bigSpawnPoints[Random.Range(0, bigSpawnPoints.Count)];
        Instantiate(bigCratePrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void OnDestroy()
    {
        isRunning = false;
    }
}
