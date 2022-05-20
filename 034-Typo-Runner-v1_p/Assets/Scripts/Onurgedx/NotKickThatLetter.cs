using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class NotKickThatLetter : DesiredLetter
{

    public GameObject targetGo;

    public bool kickAble = true;
    
    
    
        
    
    public override string m_ClassName
    {
        get
        {
            return "NotKickThatLetter";
        }
    }

    protected override void ExploderCreate()
    {
        base.ExploderCreate();
        if(kickCount>0)
        {
        //GoExp.transform.parent = TWWordCreater.m_groundAmaUtangacTransform;
        
        }

    }


    protected override void ItIsWorkInStart()
    {
        base.ItIsWorkInStart();
        ChangeColorPls(Color.yellow);
        SetTagKick();

        
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && kickAble)
        {
            kickAble = false;
            
            StartCoroutine( goDesiredPlace(other.gameObject));


        }

    }

    


    public void stepstepNext()
    {


       
        
        if (kickCount == 1 ||TWWordCreater.nextWord.GetComponent<WordCreater>().typeOfWord!="kick" ||  TWWordCreater == null || TWWordCreater.nextWord==null  )
                {
                    SetOffCollider();
                    SetOffMeshRenderer();
                    GoExp.transform.position = transform.position;
                    GoExp.transform.localScale /= 2f;
                    ColliderParentForce(true,1f,-1f,false);
                }
        else if( TWWordCreater.nextWord.GetComponent<WordCreater>().typeOfWord=="kick")
                {
                    SettingFamilyThings(TWWordCreater.nextWord);
                    // askiya alindi
                   //TWWordCreater.nextWord.GetComponent<WordCreater>().SetKickAbleLetter(transform.position,gameObjectFirstLetter,rotateAngle,transform.rotation); 
                }

         kickCount++;
         kickAble = true;
         ChangeColorPls(Color.yellow);

         StartCoroutine(selfDestroy(5f));
    }

    public bool IsItOkeyForWord
    {
        get
        {
            targetGo = TWWordCreater.unwLetterScript.gameObject;

            return TWWordCreater.unwLetterScript.gameObjectFirstLetter== gameObjectFirstLetter;
        }
    }
    

    private IEnumerator goDesiredPlace(GameObject playerGo)
    {
        if(!IsItOkeyForWord)// uygunsa giriyor 
        {
        //GetComponent<Collider>().enabled = false;

        afterFalseChoise(playerGo);
        
        

     //   Vector3 desiredPos = Vector3.RotateTowards(-transform.forward, transform.position - TrueWord.GetComponent<Collider>().bounds.center, 0.3f*rotateAngle,0.0f);
        //Vector3 point0 = transform.position;
       StartCoroutine( rotateAroundWheel(gameObject,-1.3f*rotateAngle));
       
        Vector3 offsetXAccordingPlayergo = new Vector3(transform.position.x -playerGo.transform.position.x  , 0, 0);
        
        Vector3 desiredPos = transform.position +offsetXAccordingPlayergo*1.5f;

        Quaternion desiredQuaternion = transform.rotation;
        
        
        
        StartCoroutine(rotateAroundWheel(gameObject, 1.3f* rotateAngle));
        
        transform.DOJump(desiredPos, 2.7f, 1, 0.8f);//.OnComplete(()=>stepstepNext());
        
        transform.DORotate(desiredQuaternion.eulerAngles , 0.7f);
        
        GoExp.transform.parent = transform.parent;
        //Vector3 localpoint = transform.localPosition;
      
        
        float timeCounter = 1f;
        while(timeCounter<1)
        {
            
           //Quaternion. 
           StartCoroutine( rotateAroundWheel(gameObject,-Time.deltaTime* rotateAngle ));
            
            //transform.localPosition += 0.02f * Vector3.up * Mathf.Sin(2*Mathf.PI * timeCounter);
            
            //transform.position = Vector3.Lerp(point0,point0, timeCounter);
            //transform.position = Vector3.Lerp(point0, targetGo.transform.position, timeCounter);
            timeCounter += Time.deltaTime * 2;
            yield return null;
           
        }

        yield return new WaitForSeconds(0.8f);
        
        GoExp.transform.position = transform.position;
        stepstepNext();
       // SetOffMeshRenderer();
        //SetOffCollider();
        yield return new WaitForSeconds(1f);
        }
        
        else
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
            StartCoroutine(afterTrueCrush());

        }


    }
    
    private void afterFalseChoise(GameObject playerGo)
    {
       // playerGo.GetComponent<Player>().ThrowBackEksi1();
        ChangeColorPls(Color.red);
        //CurshMessageSent();
        
        
    }
    


}
