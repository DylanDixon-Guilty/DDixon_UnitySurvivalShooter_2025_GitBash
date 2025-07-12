using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    private void Start()
    { 
        EnemyAttack.player = GameObject.FindGameObjectWithTag("Player");
        EnemyMovement.playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }

    /// <summary>
    /// Spawn an enemy at enemySpawnLocation (empty game object)
    /// </summary>
    private void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
