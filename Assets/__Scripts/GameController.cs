using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //  Asteroids
    public GameObject hazard;
    public int hazardCount;
    public Vector3 spawnValues;

    public float spawnDelay;
    public float startDelay;
    public float waveDelay;
    private void Start()
    {
       StartCoroutine(SpawnWaves());
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);
        while (true) {
            for (int i = 0; i < hazardCount; i++) {
                //  Set spawn position of asteroids to be a random range between specified values in the editor (length of screen)
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnDelay);
            }
            yield return new WaitForSeconds(waveDelay);

        }
    }
}
