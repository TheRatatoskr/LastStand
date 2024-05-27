using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ActionAttack : MonoBehaviour
{

    [Header("Attack Settings")]
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float hitBoxDecay = 1f;
    [SerializeField] private AudioClip attackSound;

    [Header("Required Components")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private AudioSource audioSource;


    [Header("Debug Only Plox")]
    [SerializeField] private float sinceLastAttack = 0f;

    private bool isAlive = true;

    private void Start()
    {
        inputReader.AttackButtonPressed += KickAttack;
    }

    private void Update()
    {
        sinceLastAttack += Time.deltaTime;
        if (sinceLastAttack >= hitBoxDecay)
        {
            hitBox.SetActive(false);
        }
    }
    private void KickAttack(bool buttonIsPressed)
    {
        if(sinceLastAttack > attackDelay && buttonIsPressed) 
        {
            sinceLastAttack = 0f;
            animator.SetTrigger("attack");
            hitBox.SetActive(true);
            audioSource.clip = attackSound;
            audioSource.Play();
        }
    }

    public void PlayerIsDead()
    {
        inputReader.AttackButtonPressed -= KickAttack;
    }

    private void OnDestroy()
    {
        inputReader.AttackButtonPressed -= KickAttack;
    }
}
