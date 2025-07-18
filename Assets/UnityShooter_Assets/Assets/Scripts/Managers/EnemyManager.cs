using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemyPrefab;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public static GameObject Player;


    private void Start()
    { 
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        
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
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
