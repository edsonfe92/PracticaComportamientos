using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    public Transform cam;

    private void Awake() {
        cam = GameObject.Find("CamCenital").transform;
    }
    private void LateUpdate() 
    {
        transform.LookAt(transform.position + cam.forward);    
    }
}
