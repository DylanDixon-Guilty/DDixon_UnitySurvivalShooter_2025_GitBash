using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrapManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject trap;
    public float trapSpawnTimer = 10f;
    public Transform[] trapSpawnPoint;

    
    private void Start()
    {
        InvokeRepeating(nameof(TrapSpawning), trapSpawnTimer, trapSpawnTimer);
    }

    /// <summary>
    /// Every 10 seconds a trap will spawn at 1 of the trap spawn locations (Game-object).
    /// </summary>
    private void TrapSpawning()
    {
       if (playerHealth.currentHealth <= 0)
       {
                return;
       }

       int spawnPointIndex = Random.Range(0, trapSpawnPoint.Length);
       Instantiate(trap, trapSpawnPoint[spawnPointIndex].position, trapSpawnPoint[spawnPointIndex].rotation);
    }
}
