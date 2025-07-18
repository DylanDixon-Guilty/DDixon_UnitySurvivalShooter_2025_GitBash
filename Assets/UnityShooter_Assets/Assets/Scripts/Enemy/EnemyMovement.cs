using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;
    private Transform playerTransform;

    

    private void Awake()
    {
        playerTransform = EnemyManager.Player.transform;
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
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
            nav.SetDestination(playerTransform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
