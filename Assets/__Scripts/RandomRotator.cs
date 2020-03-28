using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This script adds a random rotation to the asteroids to make it look as if they're perpetually flying through space
public class RandomRotator : MonoBehaviour
{
    public float tumble;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //  Gves a random V3 value and set to rb's angular velocity
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
