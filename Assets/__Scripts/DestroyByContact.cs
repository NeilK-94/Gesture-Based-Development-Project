using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private GameObject gameControllerObject;

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    private void Start()
    {
        gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    //  When something enters this collider, destroy it 
    private void OnTriggerEnter(Collider other)
    {
        //  Don't blow up if we hit the boundary
        if(other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            //  Instantiate explosions
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if(other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);  //  Destroy bullet
        Destroy(gameObject);    //  Destroy asteroid
    }
}
