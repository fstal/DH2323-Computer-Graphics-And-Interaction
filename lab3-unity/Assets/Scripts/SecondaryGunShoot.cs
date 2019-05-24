﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryGunShoot : MonoBehaviour
{
    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay = new Ray();                       // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.


    void Awake()
    {
        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetMouseButton(1) && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            // ... shoot the gun.
            Shoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
    }


    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        // If it hits something, set the second position of the line renderer to the point the raycast hit, otherwise, 
        // set the second position of the line renderer to the maximal raycast range.

        if (Physics.Raycast(shootRay.origin, shootRay.direction, out shootHit, range))
        // A raycast hit to get information about what was hit.
            {
                if (shootHit.collider)
                    {
                        //Debug.Log(shootHit.collider.name);
                        gunLine.SetPosition(1, shootHit.point);
                        
                        if (shootHit.collider.name == "enemytank(Clone)")
                            {
                                //Debug.Log(shootHit.collider.gameObject);
                                //TankExplode();
                                Destroy(shootHit.collider.gameObject);
                            }

                    }
            }
            else
            {
                //Vector3 farHit = new Vector3 (shootRay.)
                //Debug.Log(shootHit.distance);
                gunLine.SetPosition(1, shootRay.origin+shootRay.direction * range);
            }

    }
}
