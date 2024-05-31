using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunTurret : MonoBehaviour, ITurretControls
{
    [Header("Objects")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem topFlash;
    [SerializeField] private ParticleSystem bottomFlash;
    
    [SerializeField] private Transform topBarrel;
    [SerializeField] private Transform bottomBarrel;

    [SerializeField] private AudioClip bangNoise;

    [Header("Settings")]
    [SerializeField] private float waitTimeBetweenShots;
    private float adjustedWaitTime;

    private bool isShooting = true;

    private bool shootFromTop = true;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        adjustedWaitTime = waitTimeBetweenShots;
        StartCoroutine(GunCycle());
    }

    IEnumerator GunCycle()
    {
        while (isShooting)
        {
            yield return new WaitForSeconds(adjustedWaitTime);
            PlayGunSound();
            if(shootFromTop)
            {
                topFlash.Play();
                ShootBullet(topBarrel);
            }
            else
            {
                ShootBullet(bottomBarrel);
                bottomFlash.Play();
            }
            adjustedWaitTime = Random.Range(waitTimeBetweenShots * .8f, waitTimeBetweenShots * 1.2f);
        }
    }

    private void ShootBullet(Transform startPosition)
    {
        Instantiate(bullet, startPosition);
        PlayGunSound();
        shootFromTop = !shootFromTop;
    }



    private void PlayGunSound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }


}
