using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprinting : MonoBehaviour
{
    //When player holds down Left-shift for at least 0.5 seconds, take away 10-stamina from currentStamina
    private int staminaDamage = 10;
    private int timerToReRun = 1; // Goes to While statement in the FixedUpdate funtion/method.
    private PlayerMovement playerMovement;

    public Slider sprintMeter;
    public bool tired; // Completely out of Stamina
    public int maxStamina = 20;
    public int currentStamina;


    public float timeToRun = 0.5f; // How long the Stamina bar takes to deplete per charge (holding down left-shift)

    private void Awake()
    {
        currentStamina = maxStamina;
        playerMovement = GetComponent<PlayerMovement>();

    }


    private void Update()
    {
        Sprinting();
    }

    /// <summary>
    /// This functions handles when the player will regain their stamina after 8 Seconds
    /// </summary>
    private void FixedUpdate()
    {
        // Countdown of when player Regains Stamina
       if(currentStamina != 20 && tired)
        {
            while (timerToReRun > 0)
            {
                timerToReRun--;
                StartCoroutine(nameof(CountdownTimer));
            }
        }

        if (currentStamina == 20)
        {
            tired = false;
        }
    }

    /// <summary>
    /// pressing Shift lets the player run
    /// </summary>
    public void Sprinting()
    {
        if (Input.GetButtonDown("Dash") && tired)
        {
            
            playerMovement.speed = 12f;
            InvokeRepeating(nameof(StaminaDrainer), timeToRun, timeToRun);
        }
        else if (currentStamina <= 0)
        {
            playerMovement.speed = 6f;
            tired = true;
            CancelInvoke(); //To stop Drain Stamina
        }
        else if (Input.GetButtonUp("Dash") && playerMovement.speed == 12f)
        {
            playerMovement.speed = 6f;
            CancelInvoke();
        }

    }

    /// <summary>
    /// To Regenarate Stamina after 8 seconds
    /// </summary>
    private IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(8);
        StaminaRegenarate();
    }

    /// <summary>
    /// To Regenerate all of player's Stamina
    /// </summary>
    private void StaminaRegenarate()
    {
        currentStamina = maxStamina;
        sprintMeter.value = currentStamina;
        tired = false;
        timerToReRun = 1; //To reset While statement
    }

    /// <summary>
    /// Function to take away Stamina from player while Sprinting
    /// </summary>
    public void DrainStamina(int amount)
    {
        currentStamina -= amount;
        sprintMeter.value = currentStamina;
        
    }

    /// <summary>
    /// To make invoke in Sprinting method easier to handle
    /// </summary>
    private void StaminaDrainer()
    {
        DrainStamina(staminaDamage);
    }
}
