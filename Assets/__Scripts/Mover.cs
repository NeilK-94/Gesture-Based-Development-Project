using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This script moves whatever its attached to down the screen
public class Mover : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //  transorm.forward is y axis
        rb.velocity = transform.forward * speed;
    }
}
