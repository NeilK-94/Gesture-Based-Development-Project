using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Pose = Thalmic.Myo.Pose;


public class GameController : MonoBehaviour
{
    //  Asteroids
    public GameObject[] hazards;
    public int hazardCount;
    public Vector3 spawnValues;
    public float spawnDelay;
    public float startDelay;
    public float waveDelay;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public GameObject myo = null;   //  The MYO Hub

    public ThalmicHub hub;
    private static ThalmicMyo myoArmband;
    private bool gameOver;
    private bool restart;
    private int score;
    private Scene currentScene;

    private void Start()
    {
        gameOver = false;
        restart = false;

        myoArmband = myo.GetComponent<ThalmicMyo>();


        //  Make text blank until needed
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        Restart();
    }

    IEnumerator SpawnWaves()
    {
        GameObject hazard = hazards[Random.Range(0, hazards.Length)];
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

            if(gameOver)
            {
                restartText.text = "Double tap to restart";
                restart = true;
                break;
            }
        }
    }

    //  Update score on screen
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
    public void Restart()
    {   //  If restart is true
        if (restart)
        {
            currentScene = SceneManager.GetActiveScene();   //  Sets current scene equal to the active scene
            if (myoArmband.pose == Pose.DoubleTap)
            {
                SceneManager.LoadScene(currentScene.name);  //  loads currentScene
                //Debug.Log(currentScene.name);
            }
        }
        
    }
}
