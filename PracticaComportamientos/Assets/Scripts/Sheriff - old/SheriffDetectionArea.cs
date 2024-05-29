using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffDetectionArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "forajido")
        {
            SheriffBH.onThiefDetected(other.gameObject);
        }
    }
}