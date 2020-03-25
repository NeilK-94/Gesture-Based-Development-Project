using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

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

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());   //  Put keys into an array. Just restart as of now
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaued = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting..");
        Application.Quit();
    }
}
