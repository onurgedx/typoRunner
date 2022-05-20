using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanishing : MonoBehaviour
{

    public GameObject mainGo;
    public float durationLife = 4f;


    private void Awake()
    {
        
    }


    private void OnEnable()
    {
        
        StartCoroutine(VanishSelfObject());

    }

    private IEnumerator VanishSelfObject()
    {
        
        //transform.position = mainGo.transform.position;
        yield return new WaitForSeconds(durationLife);
        Destroy(gameObject);
        
    }
    

}
