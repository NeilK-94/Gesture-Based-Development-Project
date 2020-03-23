using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMe : MonoBehaviour
{
    public ThalmicHub hub;

    private void Awake()    //  Call the singleton method 
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        //  find object of type musicplayer
        //  if there is one, use that instance
        //  destroy the one just created
        //  FindObjectOfType()

        if (FindObjectsOfType<ThalmicHub>().Length > 1)
        {
            Destroy(hub);
        }
        else
        {
            DontDestroyOnLoad(hub);
        }
    }
}
