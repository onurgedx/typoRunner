using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//day 10 used that
public class DesiredLetter : UnwantedLetter
{

    public GameObject player;
    
    
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CurshMessageSent();
            

        }
        
        
    }
    public override string m_ClassName
    {
        get
        {
            return "DesiredLetter";
        }
    }
    
    
}
