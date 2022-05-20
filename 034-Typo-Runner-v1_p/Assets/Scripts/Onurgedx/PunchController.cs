using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("word"))
        {
            if (other.gameObject.GetComponent<WordCreater>().typeOfWord == "kick")
            { 
               // player.Kickit();
            }
            else
            {
                 player.Punchit();
            }
            
            
            
         
        }
        else if (other.gameObject.CompareTag("kick"))
        {
            player.Kickit();
        }
    }
    
    
    
}
