using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrapManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject trap;
    public float trapSpawnTimer = 10f;
    public Transform[] trapSpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(TrapSpawning), trapSpawnTimer, trapSpawnTimer);
    }

    //Every 10 seconds a trap will spawn at 1 of the trap spawn locations.
    void TrapSpawning()
    {
       if (playerHealth.currentHealth <= 0)
       {
                return;
       }

       int spawnPointIndex = Random.Range(0, trapSpawnPoint.Length);
       Instantiate(trap, trapSpawnPoint[spawnPointIndex].position, trapSpawnPoint[spawnPointIndex].rotation);
    }
}
