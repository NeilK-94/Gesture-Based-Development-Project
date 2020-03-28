using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

//  This script listens out for keywords using the keywordRecognizer. It then activates or deactivates the pauseMenuUI 
//  It also uses the scene manager to move to main menu if needed.
//  No need to comment on most of this as it's all already been commented on it other scripts.
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaued = false;
    public GameObject pauseMenuUI;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    private void Start()
    {
        keywordActions.Add("pause", Pause);
        keywordActions.Add("resume", Resume);
        keywordActions.Add("quit", QuitGame);
        keywordActions.Add("menu", Menu);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaued = true;
    }
    public void Resume()
    {
        if (GameIsPaued)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaued = false;
        }
    }

    public void QuitGame()
    {
        if (GameIsPaued)
        {
            Debug.Log("Quitting..");
            Application.Quit();
        }
    }

    private void Menu()
    {
        if (GameIsPaued)
        {
            //  Move down the build index to the previous scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
