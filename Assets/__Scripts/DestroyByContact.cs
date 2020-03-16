using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //  When something enters this collider, destroy it 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }
        Destroy(other.gameObject);  //  Destroy bullet
        Destroy(gameObject);    //  Destroy asteroid
    }
}
