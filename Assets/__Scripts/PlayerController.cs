using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Myo dependencies
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

//  Make Boundary class visible in editor
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public float fireRate;
    public GameObject bullet;
    public Transform bulletSpawn;
    public Boundary boundary;
    public GameObject myo = null;   //  The MYO Hub

    private ThalmicMyo myoArmband;
    private Rigidbody rb;
    private Pose lastPose = Pose.Unknown;  //  Myo stores our poses. Set pose to Unknown initially.
    private AudioSource bulletClip;
    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletClip = GetComponent<AudioSource>();
        myoArmband = myo.GetComponent<ThalmicMyo>();
    }

    //  Update should be used for input
    private void Update()
    {
        Shoot();
    }
    //  FixedUpdate should be used for physics.
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //  Keyboard controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
     //////////////////////////////////////////////////////////////////////////////////
        //  Myo controls
        //  Just like keyboard input except this time we set it to the myoArmband's current transform position
        float myoMoveHorizontal = myoArmband.transform.forward.x;
        float myoMoveVertical = myoArmband.transform.forward.y;

        //  Store these in a Vector3, together, with the y axis value always at zero we can manipulate the player's position
        Vector3 myoMovement = new Vector3(myoMoveHorizontal, 0.0f, myoMoveVertical);
        rb.velocity = myoMovement * speed;

        //  Set constraints to avoid player moving out of screen
        rb.position = new Vector3
        (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        //  Rotate the ship when turning
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    private void Shoot()
    {
        if(myoArmband.pose == Pose.Fist && Time.time > nextFire)
        {
            Debug.Log("FIST BUMP!");
            nextFire = Time.time + fireRate;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            bulletClip.Play();
        }
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            bulletClip.Play();
        }
    }
}
