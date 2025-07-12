using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;

    public static Transform playerPosition; //Also used in EnemyManager

    private void Awake()
    {
        playerHealth = playerPosition.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// The enemy will walk towards the player as long as the player (or the enemy) is not dead.
    /// </summary>
    private void Update()
    {
        
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(playerPosition.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
