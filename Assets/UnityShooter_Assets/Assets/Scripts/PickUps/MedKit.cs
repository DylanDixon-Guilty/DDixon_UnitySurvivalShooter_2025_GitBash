using System.Collections;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public int Heal = 10; //Heals player for 10 Hit points
    public GameObject medKit;

    private GameObject player;
    private PlayerHealth playerHealth;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        StartCoroutine(DespawnTimer());
    }

    /// <summary>
    /// When the Medkit stays on ground for alotted amount of time (in Start function).
    /// </summary>
    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    /// <summary>
    /// When player walks over Medkit and there health is less than 100, Pick up Medkit
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject == player && playerHealth.currentHealth != 100)
        {
            playerHealth.HealthRestored(Heal);
            Destroy(gameObject);
        }
    }

}
