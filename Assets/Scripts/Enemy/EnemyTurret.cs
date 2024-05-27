using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class EnemyTurret : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform rotateHinge;
    [SerializeField] private Transform player;

    [Header("Laser Settings")]
    [SerializeField] private GameObject laserBox;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip laserFireSound;
    [SerializeField] private float laserFireVolume = .02f;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private float chargeFirePitchIncrease;

    [Header("Timer Settings")]
    [SerializeField] private float chargeTime;
    [SerializeField] private float fireDelay;
    [SerializeField] private float fireLength;
    [SerializeField] private float fireCooldown;

    [Header("Debug Only")]
    [SerializeField] private float valueImWatching;
    
    private float currentAlarm;
    private float currentTimer;
    private int gunState = 0;
    private int totalGunStates = 4;
    private bool canRotate = true;

    private void Start()
    {
        currentAlarm = chargeTime;
        UpdateSound(chargeSound);
    }

    //rotate around the hinge
    private void RotateAroundHinge()
    {
        if(canRotate)
        {
            transform.LookAt(player.position);
        }
    }

    //follow the barrel to the player

    private void Update()
    {
        LaserTimerLogic();
        RotateAroundHinge();
    }

    private void UpdateSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
    private void LaserTimerLogic()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= currentAlarm)
        {
            //adds 1 to gunstate, then checks if we are out of bounds for state
            //contributed by the great mysterious Ivanstu whom i have 'absolutely' no idea who he is.
            if (++gunState >= totalGunStates) gunState = 0;

            switch (gunState) 
            {
                case 0:
                    currentTimer = 0;
                    currentAlarm = chargeTime;
                    canRotate = true;
                    UpdateSound(chargeSound);
                    break;
                case 1:
                    currentAlarm += fireDelay;
                    canRotate = false;
                    audioSource.pitch += chargeFirePitchIncrease;
                    break;
                case 2:
                    currentAlarm += fireLength;
                    laserBox.SetActive(true);
                    audioSource.volume = laserFireVolume;
                    UpdateSound(laserFireSound);
                    break;
                case 3:
                    currentAlarm += fireCooldown;
                    laserBox.SetActive(false);
                    audioSource.Stop();
                    audioSource.loop = false;
                    break;
                default:
                    Debug.LogWarning($"Gunstate of {gameObject.name} is out of range somehow!"); 
                    break;
            }
        }
    }

}
