using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Background scroller
public class BGScroller : MonoBehaviour
{

    private int zLength = 30;
    private float scrollSpeed = -1f;    //  '-' because we're going down the axis

    void Update()
    {
        transform.position = new Vector3(0.0f, -10.0f, Mathf.Repeat(Time.time * scrollSpeed, zLength));
    }
}