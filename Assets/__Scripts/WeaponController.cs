using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This script controls the enemies weapon system. How often they shoot 
public class WeaponController : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;
    public float fireRate;
    public float delay;


    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", delay, fireRate);  //  Repetitively invoke a function
    }

    void Shoot()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        audioSource.Play();
    }
}
