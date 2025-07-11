using System.Collections;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public int Heal = 10; //Heals player for 10 Hit points

    GameObject player;
    public GameObject medKit;
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

    // When the Medkit stays on ground for alotted amount of time (in Start function).
    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    //When player walks over Medkit
    private void OnCollisionEnter(Collision other)
    {
        // If player's Health is lower than 100, pickUP
        if (other.gameObject == player && playerHealth.currentHealth != 100)
        {
            playerHealth.HealthRestored(Heal);
            Destroy(gameObject);
        }
    }

}
