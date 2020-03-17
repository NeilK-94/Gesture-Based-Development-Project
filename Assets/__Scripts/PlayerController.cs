using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Rigidbody rb;
    private AudioSource bulletClip;
    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletClip = GetComponent<AudioSource>();
    }

    private void Update()
    {   //  Fire1 will be MYO etc
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            bulletClip.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

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

}
