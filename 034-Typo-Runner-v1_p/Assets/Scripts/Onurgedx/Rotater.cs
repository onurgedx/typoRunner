using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



// DAY 3 USED THIS
public class Rotater : MonoBehaviour
{
    public Transform player;
    public float selfSpeedOfPlayer;

    public float durationOfArrive4Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     //   transform.position =new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z + 0.51209f);// player.transform.position+ player.transform.forward*0.51209f  ;  

    }
    public void MovePlayer(float AspectOfMove)
    {
        if(Math.Abs(transform.localPosition.x + selfSpeedOfPlayer*AspectOfMove*Time.fixedDeltaTime) <=1.4f)
        
        {transform.Translate(selfSpeedOfPlayer*AspectOfMove*Time.fixedDeltaTime,0,0);}
        else
        {
            transform.localPosition = new Vector3(Math.Sign(AspectOfMove)*1.4f,transform.localPosition.y,transform.localPosition.z);
        }
    }

    public void DirectSetPosition(float xPos)
    {
        //transform.position = new Vector3( xPos, transform.position.y, transform.position.z);

        //transform.DOMove(new Vector3(xPos, transform.position.y, transform.position.z), durationOfArrive4Enemy);
            // bu yanlis calisti cunku transform .position.y deddgimizde mesela  ilk dedigimiz yere gidiyor
            transform.DOMoveX(xPos,durationOfArrive4Enemy);

    }
    
    
    
    
}
