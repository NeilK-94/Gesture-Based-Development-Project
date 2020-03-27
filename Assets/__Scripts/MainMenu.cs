using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

//  Myo dependencies
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;


public class MainMenu : MonoBehaviour
{
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    public GameObject myo = null;   //  The MYO Hub
    private ThalmicMyo myoArmband;
    private Pose lastPose = Pose.Unknown;
    public AudioMixer soundTrack;
    public AudioMixer soundEffects;

    public static bool isMuted = false; //  Check to see if the audio is muted
    private float mute = -80f;
    private float vol = 0;


    void Start()
    {
        keywordActions.Add("start", StartGame);
        keywordActions.Add("quit", QuitGame);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());   //  Put keys into an array. Just restart as of now
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();

        myoArmband = myo.GetComponent<ThalmicMyo>();
    }
    void Update()
    {
        if (myoArmband.pose != lastPose)
        {
            lastPose = myoArmband.pose;

            if (myoArmband.pose == Pose.FingersSpread)
            {
                //  Disable audio mixer
                Debug.Log("Finger Spread");
                lastPose = Pose.FingersSpread;
                
                MuteVolume();
                ExtendUnlockAndNotifyUserAction(myoArmband);
            }
            else if (myoArmband.pose == Pose.WaveOut)
            {
                Debug.Log("wave out");
                lastPose = Pose.WaveOut;

                MuteAffects();
                ExtendUnlockAndNotifyUserAction(myoArmband);
            }
        }

        
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting..");
        Application.Quit();
    }    

    public void MuteVolume()
    {
        if (isMuted == false)    //  if the audio is not muted, mute it
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
    {
        if (isMuted == false)    //  if the audio is not muted, mute it
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

    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}
