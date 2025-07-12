using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public static GameObject player; //Also used in EnemyManager

    private Animator anim;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private bool playerInRange;
    private float timer;

    private void Awake()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// When enemy gets close enough to player
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    /// <summary>
    /// When player steps away from the enemy
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) 
        {
            playerInRange = false;
        }
    }

    
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    /// <summary>
    /// Enemy hurts player
    /// </summary>
    private void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
