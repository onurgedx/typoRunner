using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    public GameObject cameraLook;
    // Start is called before the first frame update
    void Start()
    {
        if (cameraLook == null)
        {
            cameraLook = Camera.main.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
       // transform.LookAt(cameraLook.transform);
        transform.rotation = Quaternion.LookRotation(cameraLook.transform.position-transform.position);
        
        
    }

     
}
