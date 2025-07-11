using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectDisplayTime = 0.2f;
    
    //For swapping between guns
    public GameObject gun;
    public GameObject shotGun;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    // Update is called once per frame
    void Update()
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

    // Pressing One and Two swaps between guns
    void ChoosingGun()
    {
        if(Input.GetButtonDown("ItemOne"))
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

    //Pressing Left-Click fires the gun
    void Shoot()
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


    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
