using UnityEngine;

public class Traps : MonoBehaviour
{
    public GameObject player;

    public PlayerHealth playerHealth;
    Animator anim;

    public int trapDamage = 15;
    public float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        Invoke(nameof(TrapDespawning), 8f);
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth.currentHealth <= 0 && playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    //Hurt the player when they enter the trap once.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            DamagePlayer();
        }
    }

    
    void DamagePlayer()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(trapDamage);
        }
    }

    //After 8 seconds the trap will despawn, (in Start function).
    void TrapDespawning()
    {
        Destroy(gameObject);
    }
}
