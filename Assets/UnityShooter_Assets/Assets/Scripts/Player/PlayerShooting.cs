using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    private float timer;
    private int shootableMask;
    private float effectDisplayTime = 0.2f;
    private Ray shootRay;
    private RaycastHit shootHit;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    
    //For swapping between guns
    public GameObject gun;
    public GameObject shotGun;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    private void Update()
    {
        timer += Time.deltaTime;
                                                                        /*When menu is paused*/
        if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && PauseMenu.gameIsPaused != true)
        {
            Shoot(); 
        }
        if (timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }


        ChoosingGun();
    }

    /// <summary>
    /// Pressing One or Two swaps between guns
    /// </summary>
    private void ChoosingGun()
    {
        if (Input.GetButtonDown("ItemOne"))
        {
            gun.SetActive(true);
            shotGun.SetActive(false);
            
        }
        else if(Input.GetButtonDown("ItemTwo"))
        {
            gun.SetActive(false);
            shotGun.SetActive(true);
        }
    }

    /// <summary>
    /// Pressing Left-Click on mouse fires the gun
    /// </summary>
    private void Shoot()
    {
        timer = 0f;
        gunAudio.Play(); // plays shoot
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    /// <summary>
    /// To remove the light effect and line that appears after shooting
    /// </summary>
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
