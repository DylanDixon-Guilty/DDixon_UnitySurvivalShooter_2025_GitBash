using UnityEngine;

public class Traps : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;
    public int trapDamage = 15;

    private Animator anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        Invoke(nameof(TrapDespawning), 8f);
    }

    
    private void Update()
    {

        if (playerHealth.currentHealth <= 0 && playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    /// <summary>
    /// Hurts the player when they enter the trap once.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            DamagePlayer();
        }
    }

    /// <summary>
    /// The amount of Damage the player takes when stepping in Trap
    /// </summary>
    private void DamagePlayer()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(trapDamage);
        }
    }

    /// <summary>
    /// After 8 seconds the trap will despawn, (in Start function).
    /// </summary>
    private void TrapDespawning()
    {
        Destroy(gameObject);
    }
}
