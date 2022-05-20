using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WillBeKickedLetter : UnwantedLetter
{

    public GameObject targetGo;

    public float durationOfMainHome = 1f;
    
    
   
   
   

    
    
    public override string m_ClassName
        {
            get
            {
                return "WillBeKickedLetter";
            }
        }

    public override void crushing()
    {
        base.crushing();
        
    } 
    
    protected override void ItIsWorkInStart()
    {
        GetComponent<Collider>().isTrigger = true;
        ExploderCreate();
        setTrueWord();

        TrueWord.GetComponent<WordCreater>().colliderCenterChanging(transform.localPosition);
        ChangeColorPls(Color.yellow);
        SetTagKick();
        

    }
    
    
    
    private IEnumerator goDesiredPlace()
    {
        GetComponent<Collider>().enabled = false;
        
       // transform.DOJump(targetGo.transform.position, 1.2f, 1, 0.6f,false);
        
        
        
        
        
        transform.DORotate(Vector3.left, 1f);
        float timeCounter = 0f;
        Vector3 point0 = transform.position;
        while (timeCounter<1)
        {
            
            Vector3 m_positionLinePoint = point0 * Mathf.Pow((1 - timeCounter), 3) +
                                          (point0+ new Vector3(0*targetGo.transform.position.x/2 - point0.x *0,4.13999987f,-0.839999974f))  * Mathf.Pow(1 - timeCounter, 2) * timeCounter * 3 +
                                          3 * (1 - timeCounter) * timeCounter * timeCounter *(point0+ new Vector3(targetGo.transform.position.x/2 - point0.x,3.43000007f,2.88000011f)) + timeCounter * timeCounter * timeCounter * targetGo.transform.position;

            transform.position = m_positionLinePoint;
            //transform.position = Vector3.Lerp(point0, targetGo.transform.position, timeCounter);
            timeCounter += Time.deltaTime *2;
            yield return null;
            
        }

      //  yield return new WaitForSeconds(0.6f);
        //transform.rotation =Quaternion.identity;
        targetGo.GetComponent<MeshRenderer>().enabled = true;
        GetComponent<MeshRenderer>().enabled = false;
            afterTrueCrush2();
        


    }

    private void afterTrueCrush2()
    {
        
        StartCoroutine(afterTrueCrush());
    }
    

    private void OnCollisionEnter(Collision other)
    {

        
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(goDesiredPlace());


        }
    }

    private void OnTriggerEnter(Collider other)
    {if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(goDesiredPlace());


        }
        
    }
}
