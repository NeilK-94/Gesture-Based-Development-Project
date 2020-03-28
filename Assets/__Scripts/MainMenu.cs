using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

//  Myo dependencies
using Pose = Thalmic.Myo.Pose;
using VibrationType = Thalmic.Myo.VibrationType;


public class MainMenu : MonoBehaviour
{
    //  Adictionary to store our keywords
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    //  The keywordRecognizer that will listen for input
    private KeywordRecognizer keywordRecognizer;

    //  The Myo hub
    public GameObject myo = null;
    private ThalmicMyo myoArmband;

    //  The last pose the myo recorded, set to unknown initially.
    private Pose lastPose = Pose.Unknown;

    //  The audio mixer's controlling our soundtrack and sound effects
    public AudioMixer soundTrack;
    public AudioMixer soundEffects;

    public static bool isMuted = false;

    //  The mute and volume settings for our audio mixers.
    private float mute = -80f;  
    private float vol = 0;


    void Start()
    {
        //  Add the key value pairs to the dictionary 
        keywordActions.Add("start", StartGame);
        keywordActions.Add("quit", QuitGame);

        //  Reference to new keywordRecognizer and pass in the keys of our dictionary (the actual words to listen for)
        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        //  Once it recognises a phrase, do what?
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        //  KeywordRecognizer is set up, now start listening.
        keywordRecognizer.Start();

        //  Reference to Myo armband in scene
        myoArmband = myo.GetComponent<ThalmicMyo>();
    }
    void Update()
    {
        if (myoArmband.pose != lastPose)
        {
            lastPose = myoArmband.pose;

            //  If the current pose == to fingerSpread
            if (myoArmband.pose == Pose.FingersSpread)
            {
                Debug.Log("Finger Spread");
                //  Set last post to current pose
                lastPose = Pose.FingersSpread;
                //  Vibrate the armband for a long vibration type
                myoArmband.Vibrate(VibrationType.Long);

                MuteVolume();
            }
            else if (myoArmband.pose == Pose.WaveOut)
            {
                Debug.Log("wave out");
                lastPose = Pose.WaveOut;
                myoArmband.Vibrate(VibrationType.Medium);

                MuteAffects();
            }
        }
    }

    //  Our method take a phrase recognised event
    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        //  Print to console
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    public void StartGame()
    {
        //  Load the next scene in the build settings.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //  'start' time
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting..");
        //  Quit application
        Application.Quit();
    }    

    //  Method to mute soundtrack mixer
    public void MuteVolume()
    {   //  If the audio isn't already muted
        if (isMuted == false)
        {
            soundTrack.SetFloat("music", mute);
            isMuted = true; //  Set isMuted to true 
        }
        else
        {
            soundTrack.SetFloat("music", vol);
            isMuted = false;
        }
        
    }

    public void MuteAffects()
    {   //  If the audio isn't already muted
        if (isMuted == false)
        {
            soundEffects.SetFloat("soundEffects", mute);
            isMuted = true;
        }
        else
        {
            soundEffects.SetFloat("soundEffects", vol);
            isMuted = false;
        }
    }


}
