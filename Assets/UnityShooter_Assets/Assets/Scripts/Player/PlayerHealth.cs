using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float flashSpeed = 5f;
    public Slider healthSlider;
    public Image damageImage;
    public Image healImage;
    public AudioClip deathClip;
    public Color flashColour = new Color(1f, 0f, 0f, .1f);
    public Color healColor = new Color(0f, 1f, 0f, .1f);

    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private bool isDead;
    private bool damaged;

    //For MedKit Script//
    public bool isHealed;

    //For Getting Shot//
    private ParticleSystem hitParticles;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;

        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    
    private void Update()
    {
        if (damaged)//When getting damaged
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        if(isHealed)//When picking up Medkit
        {
            healImage.color = healColor;
        }
        else
        {
            healImage.color = Color.Lerp(healImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isHealed = false;
    }

    public void TakeDamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play(); // Plays Hurt
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    //When Player is shot by ZomBunny//
    public void TakeShot(int amount, Vector3 hitPoint)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play(); // Plays Hurt
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    /// <summary>
    /// Function to call upon player dying
    /// </summary>
    private void Death()
    {
        isDead = true;
        playerShooting.DisableEffects();
        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play(); // Plays Death
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    /// <summary>
    /// A Debug to let me know that the game restarted
    /// </summary>
    public void RestartLevel()
    {
        Debug.Log("Restart Level");
    }

    /// <summary>
    /// Function called upon when player walks into a Medkit
    /// </summary>
    /// <param name="amount"></param>
    public void HealthRestored(int amount)
    {
        isHealed = true;
        currentHealth += amount;
        healthSlider.value = currentHealth;
    }
}
