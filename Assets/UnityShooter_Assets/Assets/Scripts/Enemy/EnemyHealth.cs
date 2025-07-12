using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;
    public float sinkSpeed = 2.5f;
    public bool isDead;
    public bool isSinking;
    public AudioClip deathClip; // Enemy death sound

    private Animator anim;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    
    private void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// When enemy is hurt, take health away
    /// </summary>
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        enemyAudio.Play(); // plays Hurt
        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// Function for when current health of enemy reaches Zero
    /// </summary>
    private void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play(); // plays Death
    }

    /// <summary>
    /// Function to make enemy sink beneath floor and despawn
    /// </summary>
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
