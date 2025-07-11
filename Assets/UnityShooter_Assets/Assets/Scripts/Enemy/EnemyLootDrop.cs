using System.Collections;
using System.Data;
using UnityEngine;

public class EnemyLootDrop : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    int dropChance;
    
    bool stopLooting = true;
    public GameObject medKit;
    public Transform enemyDeathPoint;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.currentHealth <= 0 && stopLooting == true)
        {
            HealthKitLootOnDeath();
            stopLooting = false;
        }

    }

    //Upon defeating an enemy a Medkit will have a 50% of dropping
    void HealthKitLootOnDeath()
    {
        // Determine if a medkit will be dropped
        dropChance = 50;
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
