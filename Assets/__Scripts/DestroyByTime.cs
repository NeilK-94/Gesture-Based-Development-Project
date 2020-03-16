using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    //  Life time of game object attached to
    public float lifeTime;

    void Start()
    {   //  Destroy game object attached to after lifetime is up
        Destroy(gameObject, lifeTime);
    }
}
