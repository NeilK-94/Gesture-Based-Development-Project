using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    //  Simply destroy whatever enters the trigger
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
