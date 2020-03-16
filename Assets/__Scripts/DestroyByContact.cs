using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    //  When something enters this collider, destroy it 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }
        //  Instantiate explosions
        Instantiate(asteroidExplosion, transform.position, transform.rotation);
        if(other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

        }
        Destroy(other.gameObject);  //  Destroy bullet
        Destroy(gameObject);    //  Destroy asteroid
    }
}
