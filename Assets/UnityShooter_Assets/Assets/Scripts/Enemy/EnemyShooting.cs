using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 5f;
    public float range = 100f;

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectDisplayTime = 0.1f;
    
    private void Awake()
    {
        shootableMask = LayerMask.GetMask("ShootablePlayer");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenBullets)
        {
            Shoot();
        }
        if (timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }
    }

    /// <summary>
    /// Every 5 seconds the enemy will attempt to shoot at the player
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
            PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeShot(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    /// <summary>
    /// Function to disable the light and Gunline after Zombunny fires
    /// </summary>
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
