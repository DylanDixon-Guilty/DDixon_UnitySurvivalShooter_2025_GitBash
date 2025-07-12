using System.Collections;
using System.Data;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private bool stopLooting = true;

    public GameObject medKit;
    public Transform enemyDeathPoint;
    public int dropChance; // 50 in Enemy Prefab

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        
    }

    private void Update()
    {
        if(enemyHealth.currentHealth <= 0 && stopLooting == true)
        {
            HealthKitLootOnDeath();
            stopLooting = false;
        }

    }

    /// <summary>
    /// Upon defeating an enemy a Medkit will have a 50% of dropping
    /// </summary>
    void HealthKitLootOnDeath()
    {
        // Determine if a medkit will be dropped
        float drop = Random.Range(0f, 100f);
        if (dropChance > drop)
        {
            Instantiate(medKit, enemyDeathPoint.position, enemyDeathPoint.rotation);
        }
        else
        {
            stopLooting = true;
        }
    }
}
