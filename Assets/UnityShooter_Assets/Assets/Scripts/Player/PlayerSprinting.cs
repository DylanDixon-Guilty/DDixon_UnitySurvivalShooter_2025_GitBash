using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprinting : MonoBehaviour
{
    public int maxStamina = 20;
    public int currentStamina;
    private int staminaDamage = 10; //When player holds down Left-shift for at least 0.5 seconds, take away 10-stamina from currentStamina

    private int timer = 1; // Goes to While statement in the FixedUpdate funtion/method.
    
    public Slider sprintMeter;
    private PlayerMovement playerMovement;

    public bool outOfStamina; //Currently not in use but may cause bug if removed.
    public bool tired; // Completely out of Stamina


    public float timeToRun = 0.5f; // How long the Stamina bar takes to deplete per charge (holding down left-shift)

    void Awake()
    {
        currentStamina = maxStamina;
        playerMovement = GetComponent<PlayerMovement>();

    }


    void Update()
    {
        Sprinting();
    }

    private void FixedUpdate()
    {
        // Countdown of when player Regains Stamina
       if(currentStamina != 20 && tired == true)
        {
            while (timer > 0)
            {
                timer--;
                StartCoroutine(nameof(CountdownTimer));
            }
        }

        if (currentStamina == 20)
        {
            outOfStamina = false;
            tired = false;
        }
        else if (currentStamina < 20)
        {
            outOfStamina = true;
        }
    }

    // pressing Shift lets the player run
    public void Sprinting()
    {
        if (Input.GetButtonDown("Dash") && tired == false)
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

    //To Regenarate Stamina after 8 seconds//
    private IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(8);
        StaminaRegenarate();
    }
    void StaminaRegenarate()
    {
        currentStamina = maxStamina;
        sprintMeter.value = currentStamina;
        tired = false;
        timer = 1; //To reset While statement
    }


    public void DrainStamina(int amount)
    {
        currentStamina -= amount;
        sprintMeter.value = currentStamina;
        
    }
    //To make invoke in Sprinting method easier to handle
    void StaminaDrainer()
    {
        DrainStamina(staminaDamage);
    }
}
