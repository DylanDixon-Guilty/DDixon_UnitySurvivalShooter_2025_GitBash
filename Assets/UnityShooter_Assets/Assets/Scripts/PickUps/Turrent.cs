using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turrent : MonoBehaviour
{
    //The part of the turret that rotates//
    public Transform turretHead;

    private Transform target;
    private GameObject[] enemiesInRange;
    private Transform currentTarget;

    //For Shooting the enemy//
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.4f;
    public float range = 100f;

    private Ray shootRay;
    private RaycastHit shootHit;
    private float timer;
    private int shootableMask;
    private float effectDisplayTime = 0.2f;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;

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
        //For Looking At Enemy//
        UpdateCurrentTarget();
        LookingAtTarget();

        //For Attacking//
        timer += Time.deltaTime;

        if (currentTarget != null && timer >= timeBetweenBullets)
        {
            Shoot();
        }
        if (timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }
    }

    /// <summary>
    /// When there is more than one target, it will target the one that newly entered the radius (trigger Sphere collider)
    /// </summary>
    private void UpdateCurrentTarget()
    {

        enemiesInRange = GameObject.FindGameObjectsWithTag("Enemy");
        //To stop firing at enemy that is dead//
        if (enemiesInRange.Length == 0)
        {
            currentTarget = null;
            return;
        }

        //Select the closest enemy//
        float closestDistance = float.MaxValue;
        Transform closest = null;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }
        currentTarget = closest;
    }

    /// <summary>
    /// For When the turret can see Enemy
    /// </summary>
    private void LookingAtTarget()
    {
        if (currentTarget != null)
        {
            //Rotate turret head
            Vector3 direction = (currentTarget.position - turretHead.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    /// <summary>
    /// Every 0.5 seconds the Turrent will attempt fire at the enemy if they are within range
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
            if (enemyHealth != null)
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
    /// Function to disable Light and gunline when turret is shooting
    /// </summary>
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
