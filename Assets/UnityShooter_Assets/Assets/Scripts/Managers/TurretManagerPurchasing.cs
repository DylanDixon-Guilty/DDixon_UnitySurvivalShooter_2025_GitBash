using System.Collections;
using UnityEngine;

public class TurretManagerPurchasing : MonoBehaviour
{
    public bool turretInLevel; // To check if there is a Turret in the level
    public GameObject turret;
    public GameObject purchase;
    public Transform turretLocation;
    public int takePoints = 150;

    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>(); // To handle removing points from the score
    }

    /// <summary>
    /// When player purchases they must wait for 10 seconds. That is when the turret will also despawn (Function found in TurretDespawn)
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(10);
        turretInLevel = false;
    }

    
    private void Update()
    {
        TurretText();
        PurchasingTurret();
    }

    /// <summary>
    /// When player reaches 150 score-points, Text on top right will appear
    /// </summary>
    private void TurretText()
    {
        if(ScoreManager.score >= 150)
        {
            purchase.SetActive(true);
        }
        else if(ScoreManager.score <=150)
        {
            purchase.SetActive(false);
        }
    }

    /// <summary>
    /// When player presses spacebar, a Turret will appear in the middle of the level
    /// </summary>
    public void PurchasingTurret()
    {
        if(Input.GetButtonDown("PurchaseTurret") && turretInLevel)
        {
            ScoreManager.score -= takePoints;
            turretInLevel = true;
            StartCoroutine(CoolDown());
            Instantiate(turret, turretLocation.position, turretLocation.rotation);
        }
    }
}
