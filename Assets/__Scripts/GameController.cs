using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //  Asteroids
    public GameObject hazard;
    public Vector3 spawnValues;

    private void Start()
    {
        SpawnWaves();
    }
    void SpawnWaves()
    {
        //  Set spawn position of asteroids to be a random range between specified values in the editor (length of screen)
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazard, spawnPosition, spawnRotation);
    }
}
