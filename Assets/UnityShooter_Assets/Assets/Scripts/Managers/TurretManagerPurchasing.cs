using System.Collections;
using UnityEngine;

public class TurretManagerPurchasing : MonoBehaviour
{
    public bool turretInLevel; // To check if there is a Turret in the level
    public GameObject turret;
    public GameObject purchase;
    public Transform turretLocation;
    private ScoreManager scoreManager;

    public int takePoints = 150;


    void Awake()
    {
        scoreManager = GetComponent<ScoreManager>(); // To handle removing points from the score
    }

    //When player purchases they must wait for 10 seconds. That is when the turret will also despawn
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(10);
        turretInLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        TurretText();
        PurchasingTurret();
    }

    //When player reaches 150 score-points, Text on top right will appear
    void TurretText()
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

    //When player presses spacebar, a Turret will appear in the middle of the level
    public void PurchasingTurret()
    {
        if(Input.GetButtonDown("PurchaseTurret") && turretInLevel == false)
        {
            ScoreManager.score -= takePoints;
            turretInLevel = true;
            StartCoroutine(CoolDown());
            Instantiate(turret, turretLocation.position, turretLocation.rotation);
        }
    }
}
