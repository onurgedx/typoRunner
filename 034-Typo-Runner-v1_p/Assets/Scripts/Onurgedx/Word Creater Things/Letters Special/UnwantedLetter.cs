using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using DG.Tweening;
using JetBrains.Annotations;
using MoreMountains.Tools;
using Unity.Mathematics;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = System.Random;

//day8 used that
public class UnwantedLetter : MonoBehaviour
{
    protected GameObject TrueWord;
    protected GameObject GoExp;
    protected WordCreater TWWordCreater;
    public GameObject GoKick;
    public float rotateAngle;

    public bool additional = false;
    
    public int kickCount = 0;
   

    [Serializable]
    public class Kickable
    {
        
        public GameObject goKickable; 
        public float rotateAmount;
        
        
        public Kickable(GameObject go , float angle)
        {
            goKickable = go;
            rotateAmount = angle;
        }
        
        
       

    }

    public List<Kickable> Kickables = new List<Kickable>();
    
    
    
    
    
    
    
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ItIsWorkInStart();

                        
                        
                        
    }

    protected virtual IEnumerator selfDestroy(float t)
    {

        yield return new WaitForSeconds(t);
        Destroy(gameObject);
        
    }
    

    protected virtual void SettingFamilyThings(GameObject trwordGo)
    {
        TrueWord = trwordGo;

        TWWordCreater = TrueWord.GetComponent<WordCreater>();
        //TWWordCreater.unwLetterScript.Kickables
        //parent

        //


    }
    
    public virtual void SetTagKick()
    {
        gameObject.tag = "kick";
    }

    protected virtual void ItIsWorkInStart()
    {           
        
        
                ExploderCreate();
        
                setTrueWord();

                KickIfThere();
                
    }
    
    


    protected virtual void ExploderCreate()
    {
        // BAS HARFINE GORE ARIYIP BULUP KOYUYOR  
        
        Transform transParent;
        
        float angleAccordingParent;
        
        if (TrueWord == null)
        {
            transParent  =  transform.parent;
            angleAccordingParent = -180;
        }
        else
        {
            
          transParent =  TrueWord.transform;
          angleAccordingParent = 0;
          
        }
        
        GoExp =Instantiate(GameObject.Find( gameObjectFirstLetter + " exp"), transform.position, Quaternion.Euler(0, angleAccordingParent, 0), transParent); 
        //
        GoExp.SetActive(false);   
        
        GoExp.AddComponent<Vanishing>();
        GoExp.GetComponent<Vanishing>().mainGo = gameObject;
        

    }

    public virtual void setLocalScaleHalf(float duration)
    {
        transform.DOScale(transform.localScale * 0.5f, duration);
    }

    protected virtual void setTrueWord()
    {
        
        
        if(TrueWord == null){
        TrueWord = transform.parent.gameObject;
        }
       
        TWWordCreater = TrueWord.GetComponent<WordCreater>();
    }

    public virtual void KickIfThere(float aspectForwardMin =1.5f , float aspectForwardMax = 2.7f)
    {
        if (isItKick && isItUnwantedLeter)
        {

     
            List<int> sayilar = new List<int>(){-1,0,1};

            
            sayilar.MMShuffle();
            
            float aspectForwardBack = UnityEngine.Random.Range(aspectForwardMin, aspectForwardMax);
            float meterByoneDegree = Mathf.PI * TWWordCreater.CollOfWheel.bounds.size.z / 360;
            GoKick =Instantiate(GameObject.Find(gameObjectFirstLetter) ,new Vector3(TrueWord.transform.position.x , transform.position.y,transform.position.z)   + TrueWord.transform.right* 0.1f *sayilar[0],transform.rotation,TrueWord.transform);
            float rotateAngleLetter = 3f * aspectForwardBack / meterByoneDegree;
            
            
           // rotateAroundWheel(GoKick,3f*aspectForwardBack/meterByoneDegree);
                                              //  GoKick.transform.RotateAround(TWWordCreater.CollOfWheel.bounds.center,-Vector3.right,3f*aspectForwardBack/meterByoneDegree );
            
                                              
            WillBeKickedLetter wbkLetter = GoKick.AddComponent<WillBeKickedLetter>();
            wbkLetter.targetGo = gameObject;
            wbkLetter.rotateAngle = rotateAngleLetter;
            TWWordCreater.indexOfNewUnwantedIndex =  GoKick.transform.GetSiblingIndex();

           
            Kickables.Add( new Kickable(GoKick,rotateAngleLetter ));
            sayilar.RemoveAt(0);
                    
            GetComponent<MeshRenderer>().enabled = false;
            
            AddFalseLetter(sayilar[0],aspectForwardMin: aspectForwardBack,AspectForwardMax: aspectForwardMax);
            
            sayilar.RemoveAt(0);
            AddFalseLetter(sayilar[0],aspectForwardMin: aspectForwardBack,AspectForwardMax: aspectForwardMax);
            

        }
        
    }

    protected virtual void AddFalseLetter(float RightLeftAspect=1, float UpDownAspect =1,float aspectForwardMin =1.5f ,float AspectForwardMax=2.7f)
    {
        float aspectForwardBack = UnityEngine.Random.Range(1.5f, AspectForwardMax);

      //  Vector3 falseLetterPos = TrueWord.transform.position + TrueWord.transform.up*0.04f*UpDownAspect*aspectForwardBack*0  - TrueWord.transform.forward * 0.4f*aspectForwardBack +   TrueWord.transform.right * 0.1f * RightLeftAspect;
        Vector3 falseLetterPos = new Vector3(TrueWord.transform.position.x , transform.position.y,transform.position.z) +   TrueWord.transform.right*0.1f * RightLeftAspect;  // TrueWord.transform.position  +   TrueWord.transform.right * 0.1f * RightLeftAspect;
       // Vector3 falseLetterPos = GoKick.transform.position +   GoKick.transform.right * 0.1f * RightLeftAspect; // TrueWord.transform.position  +   TrueWord.transform.right * 0.1f * RightLeftAspect;
        
        float meterByoneDegree = Mathf.PI * TWWordCreater.CollOfWheel.bounds.size.z / 360;

        float rotateAngleLetter = 3f * aspectForwardBack / meterByoneDegree;
       // TrueWord.transform.forward * 0.4f*aspectForwardBack/meterByoneDegree
        //falseLetterPos = TWWordCreater.CollOfWheel.bounds.ClosestPoint(falseLetterPos);

        
       CreatingAndSettingKickables(falseLetterPos,randomLetter,rotateAngleLetter,null,0,Quaternion.identity); 
        /*
        GameObject go;
        go = Instantiate(
            GameObject.Find(randomLetter),
            falseLetterPos,
            transform.rotation,
            TrueWord.transform);

       // Kickables.Add(new Kickable(go,rotateAngleLetter));
        //KickableList.Add(go);
        //KickableAngle.Add( 3f * aspectForwardBack / meterByoneDegree);

        //RaycastHit hit;
    //if(Physics.Raycast(go.transform.position, Vector3.back, out hit, Mathf.Infinity, Color.yellow))
        
    
                         //  rotateAroundWheel(go,3f*aspectForwardBack/meterByoneDegree);//go.transform.RotateAround(TWWordCreater.CollOfWheel.bounds.center,-Vector3.right,3f*aspectForwardBack/meterByoneDegree );
       //go.transform.Rotate(TWWordCreater.CollOfWheel.bounds.center.x * Vector3.right, 7.4f * aspectForwardBack / meterByoneDegree, Space.Self);
     
       //go.transform.parent = TrueWord.transform;
      
       
      

       
        go.AddComponent<NotKickThatLetter>();
        go.GetComponent<NotKickThatLetter>().targetGo = gameObject;
        go.GetComponent<NotKickThatLetter>().rotateAngle = rotateAngleLetter;
        Kickables.Add(new Kickable(go,rotateAngleLetter));
       // go.GetComponent<NotKickThatLetter>().BeNormalSizeFromZero();
        
        
        // go.GetComponent<NotKickThatLetter>().rotateAmount = 0.4f * aspectForwardBack / meterByoneDegree;
*/
    }

    public virtual void CreatingAndSettingKickables(Vector3 falseLetterPos,string letterForLetter,float rotateAngleLetter,[CanBeNull] Transform transParent,int t_kickCount, Quaternion rQuaternion)
    {
        if (transParent == null)
        {
            transParent = TrueWord.transform;
        }
        
        GameObject go;
        go = Instantiate(
            GameObject.Find(letterForLetter),
            falseLetterPos,
            transform.rotation,
            transParent);
        
        // Kickables.Add(new Kickable(go,rotateAngleLetter));
        //KickableList.Add(go);
        //KickableAngle.Add( 3f * aspectForwardBack / meterByoneDegree);

        //RaycastHit hit;
        //if(Physics.Raycast(go.transform.position, Vector3.back, out hit, Mathf.Infinity, Color.yellow))
        
    
        //  rotateAroundWheel(go,3f*aspectForwardBack/meterByoneDegree);//go.transform.RotateAround(TWWordCreater.CollOfWheel.bounds.center,-Vector3.right,3f*aspectForwardBack/meterByoneDegree );
        //go.transform.Rotate(TWWordCreater.CollOfWheel.bounds.center.x * Vector3.right, 7.4f * aspectForwardBack / meterByoneDegree, Space.Self);
     
        //go.transform.parent = TrueWord.transform;
      
       

       if(letterForLetter !=gameObjectFirstLetter )
       {
       NotKickThatLetter nkTLetter = go.AddComponent<NotKickThatLetter>();
        nkTLetter.targetGo = gameObject;
        nkTLetter.rotateAngle = rotateAngleLetter;
        nkTLetter.kickCount = t_kickCount;
        nkTLetter.TrueWord = TrueWord;//TWWordCreater.nextWord;
        
            
        Kickables.Add(new Kickable(go,rotateAngleLetter));
        }
       else
       {
           WillBeKickedLetter wbkLetter = go.AddComponent<WillBeKickedLetter>();
           wbkLetter.targetGo = gameObject;
           wbkLetter.rotateAngle = rotateAngleLetter;
           TWWordCreater.indexOfNewUnwantedIndex =  go.transform.GetSiblingIndex();
           wbkLetter.TrueWord = TrueWord;//TWWordCreater.nextWord;
           
           Kickables.Add( new Kickable(go,rotateAngleLetter ));
       }

       if(transParent!= TrueWord.transform)
       {
       go.transform.localScale = new Vector3(7500f, 10000f, 10000f);
       //go.transform.rotation = rQuaternion;
       }
       // go.transform.parent = TWWordCreater.m_groundAmaUtangacTransform;



    }
     public virtual void CreatingAndSettingKickables2(Vector3 falseLetterPos,string letterForLetter,float rotateAngleLetter,[CanBeNull] Transform transParent,int t_kickCount, Quaternion rQuaternion)
    {
        if (transParent == null)
        {
            transParent = TrueWord.transform;// nextword may
        }
        
        GameObject go;
        go = Instantiate(
            GameObject.Find(letterForLetter),
            falseLetterPos,
            rQuaternion,
            transParent);
        
        // Kickables.Add(new Kickable(go,rotateAngleLetter));
        //KickableList.Add(go);
        //KickableAngle.Add( 3f * aspectForwardBack / meterByoneDegree);

        //RaycastHit hit;
        //if(Physics.Raycast(go.transform.position, Vector3.back, out hit, Mathf.Infinity, Color.yellow))
        
    
        //  rotateAroundWheel(go,3f*aspectForwardBack/meterByoneDegree);//go.transform.RotateAround(TWWordCreater.CollOfWheel.bounds.center,-Vector3.right,3f*aspectForwardBack/meterByoneDegree );
        //go.transform.Rotate(TWWordCreater.CollOfWheel.bounds.center.x * Vector3.right, 7.4f * aspectForwardBack / meterByoneDegree, Space.Self);
     
        //go.transform.parent = TrueWord.transform;
      
       

       if(letterForLetter !=gameObjectFirstLetter )
       {
       NotKickThatLetter nkTLetter = go.AddComponent<NotKickThatLetter>();
        nkTLetter.targetGo = gameObject;
        nkTLetter.rotateAngle = rotateAngleLetter;
        nkTLetter.kickCount = t_kickCount;
        nkTLetter.TrueWord = TrueWord;//TWWordCreater.nextWord;
        
            
        Kickables.Add(new Kickable(go,rotateAngleLetter));
        }
       else
       {
           WillBeKickedLetter wbkLetter = go.AddComponent<WillBeKickedLetter>();
           wbkLetter.targetGo = gameObject;
           wbkLetter.rotateAngle = rotateAngleLetter;
           TWWordCreater.indexOfNewUnwantedIndex =  go.transform.GetSiblingIndex();
           wbkLetter.TrueWord = TrueWord;//TWWordCreater.nextWord;
           
           Kickables.Add( new Kickable(go,rotateAngleLetter ));
       }

       if(transParent!= TrueWord.transform)
       {
           
       go.transform.localScale = new Vector3(7499.99854f, 10000f, 10000.001f);
    //   go.transform.rotation = rQuaternion;

       }
       // go.transform.parent = TWWordCreater.m_groundAmaUtangacTransform;



    }


    public virtual void rotateAroundWheelList()
    {
       
        foreach (Kickable ka in Kickables)
        {
            StartCoroutine(rotateAroundWheel(ka.goKickable,ka.rotateAmount));
            
        }
    }

    public virtual void DestroyKickAbles()
    {
        foreach (Kickable ka in Kickables) 
        {
            if (ka.goKickable.TryGetComponent(out NotKickThatLetter nktletter))
            {
                if (nktletter.kickCount == 0)
                {
                    Destroy(ka.goKickable);
                }

            }
            else
            {
                Destroy(ka.goKickable);
            }
        }
    }

    // burasini tekerlegin icine katildiktan sonra calistir 
    public virtual void GetOutOfParentToBigParentFORKickables()
    {
        foreach (Kickable ka in Kickables)
        {
            ka.goKickable.transform.parent =
                TWWordCreater.m_groundAmaUtangacTransform; //TrueWord.transform.parent.parent.GetChild(1);

        }
        
    }
    protected virtual void BeNormalSizeFromZero()
    {
        Vector3 scaleLocal = transform.localScale;

        transform.localScale *= 0;
        
        transform.DOScale(scaleLocal, 0.4f);

    }

    protected IEnumerator  rotateAroundWheel(GameObject go , float AmountDegree)
    {
        float countAmount = 0f;
        Vector3 scaleRecord = go.transform.localScale;
        //go.transform.localScale *= 0;
        while(countAmount<=AmountDegree && false) // burayi bir sey demek icin trafige kapattim sonra bakicam icabina   
        {
            go.transform.RotateAround(TWWordCreater.CollOfWheel.bounds.center,-Vector3.right,AmountDegree*Time.deltaTime );
            //go.transform.localScale += scaleRecord * Time.deltaTime; 
            yield return null;
        }
        
        go.transform.RotateAround(TWWordCreater.CollOfWheel.bounds.center,-Vector3.right,AmountDegree);
        
        
    }
    
    public virtual string gameObjectFirstLetter
    {
        get
        {
            return gameObject.name.Substring(0, 1);
        }
    }
    
    
    
    
    public virtual string randomLetter
    {
        get
        {
            string lettersAll = "QWERTYUIOPASDFGHJKLZXCVBNM".ToUpper();
            string lettersWithoutUnwanted = lettersAll.Replace(gameObjectFirstLetter, String.Empty);

            
            return lettersWithoutUnwanted.Substring(UnityEngine.Random.Range(0, 25), 1);
        }
    }
    
    public virtual string m_ClassName
    {
        get
        {
            return "UnwantedLetter";
        }
    }


    protected virtual bool isItKick
    {
        get
        {
             return TWWordCreater.typeOfWord == "kick";
        }
        
    }
  
    protected bool isShowingAfterTrueCrush
    {
        get
        {
            return m_ClassName != "NotKickThatLetter";
        }
    }

    protected virtual bool isItUnwantedLeter
    {
        get
        {
            return m_ClassName == "UnwantedLetter";
        }
    }

    protected virtual void SetOffMeshRenderer()
    {

        GetComponent<MeshRenderer>().enabled = false;
    }

    protected virtual void SetOffCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
    
    
    
    
    
    

    // second being canvas man
    protected virtual IEnumerator afterTrueCrush()
    {
        if (TWWordCreater.isForPlayer)
        {
            TWWordCreater.setAdobted();
            TWWordCreater.setInvisible();// 3d model harfleri gorunmez yapiyor
             
            
            // TWWordCreater.m_DesiredWordText.GetComponent<Text>().text = TWWordCreater.wordStr4Canvas; // kelime bilgisini aliyor 
           // TWWordCreater.m_DesiredWordText.transform.localPosition = Vector3.zero;
           // TWWordCreater.m_DesiredWordText.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                   //    TWWordCreater.m_DesiredWordText.transform.localPosition =  Vector3.Scale( TWWordCreater.m_DesiredWordText.transform.InverseTransformPoint(TrueWord.transform.position) , new Vector3(1,1,0));
            
            
            TWWordCreater.setTextDesiredWord();

            
            Vector3 screenPos = Camera.main.WorldToScreenPoint(TrueWord.transform.position);
            float heightScreen = Screen.height;
            float widthScreen = Screen.width;
            float x = screenPos.x - (widthScreen / 2);
            float y = screenPos.y - (heightScreen / 2);
            float s = TWWordCreater.m_mainCanvas.GetComponent<Canvas>().scaleFactor;

            TWWordCreater.m_DesiredWordText.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y)/s; //   /s         
              /*   */     
             Vector3 a = Camera.main.WorldToViewportPoint(new Vector3( TWWordCreater.FirstLastLetterDistance,TrueWord.transform.position.y,TrueWord.transform.position.z));
             
             Vector3 b = Camera.main.ScreenToViewportPoint(new Vector3(TWWordCreater.m_DesiredWordText.GetComponent<RectTransform>().rect.width,0,0));

             
             
             
             float scaleX = a.x /b.x;


             TWWordCreater.m_DesiredWordText.transform.localScale = Vector3.one/ scaleX;//  Vector3.one*scaleX;
             
              
            // Vector3 desiredPosition4Word =  TWWordCreater.gameObject.transform.position + TWWordCreater.m_DesiredWordText.transform.up*2; //Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f))  +
            // Vector3 desiredPosition4Word =  TWWordCreater.gameObject.transform.localPosition + TWWordCreater.m_DesiredWordText.transform.up*6; //Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f))  +

            Vector3 desiredPosition4Word = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.8f,3f)); // HEDEF KONUM YAZININ GIDECEGI
            // TWWordCreater.m_DesiredWordText.transform.DOLocalMove(desiredPosition4Word, 1f);
             TWWordCreater.m_DesiredWordText.transform.DOMove(desiredPosition4Word, 0.8f).SetEase(Ease.InOutCubic);
             //https://easings.net/
             
             yield return new WaitForSeconds(0.3f);
             TWWordCreater.m_DesiredWordText.transform.DOScale(Vector3.one*0.2f, 0.5f);
            
            yield return new WaitForSeconds(0.5f);
            
            TWWordCreater.m_DesiredWordText.transform.DOScale(Vector3.one*0.6f, 0.4f);
//            TWWordCreater.m_DesiredWordText.transform.DOScale(Vector3.one*0.4f, 0.5f);
            yield return new WaitForSeconds(0.4f);
            TWWordCreater.m_DesiredWordText.transform.DOScale(Vector3.zero, 0.2f);



            //TWWordCreater.m_DesiredWordText.GetComponent<RectTransform>().position=   
            //RectTransformUtility.WorldToScreenPoint(Camera.main, TrueWord.transform.position);
        }
        else
        {
            TrueWord.transform.DOMoveY(TrueWord.transform.position.y + 8, 1.2f);

        }


        yield return  new WaitForSeconds(1f);
        
        SomeBreakingHeart();
    }
    protected virtual IEnumerator afterTrueCrushFirstOne()
    {
    
        
        
        //Debug.Log(TrueWord.transform.childCount); childlar kapali olsa bile goruyor onlari
        
        
        for (int i = 0; i < TrueWord.transform.childCount; i++)
        {
            
            if(TrueWord.transform.GetChild(i).gameObject.activeInHierarchy){
                if (TrueWord.transform.GetChild(i).gameObject.TryGetComponent(out NotKickThatLetter notkick))
                {
                    
                    notkick.SetOffMeshRenderer();
                    
                }
                
                
            TrueWord.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            TrueWord.transform.GetChild(i).gameObject.GetComponent<Collider>().enabled = false;
            TrueWord.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
            }
        }

        
        
        
        
         TrueWord.transform.parent = null;
        
         if(TWWordCreater.isForPlayer)
         {
             float timeCounter = 0f;
             Vector3 point0 = TrueWord.transform.localPosition;
             while (timeCounter<1)
             {
            
                 Vector3 m_positionLinePoint = point0 * Mathf.Pow((1 - timeCounter), 3) +
                                               (point0+ new Vector3(0,3.36999989f,13.1099997f) )  * Mathf.Pow(1 - timeCounter, 2) * timeCounter * 3 +
                                               3 * (1 - timeCounter) * timeCounter * timeCounter *(point0+ new Vector3(0,4.23999977f,4.53999996f)) + 
                                               timeCounter * timeCounter * timeCounter * (point0 + new Vector3(0,2.17000008f,1.52999997f));

                 TrueWord.transform.localPosition = m_positionLinePoint;
                 //transform.position = Vector3.Lerp(point0, targetGo.transform.position, timeCounter);
                 timeCounter += Time.deltaTime ;
                 yield return null;
            
             }
             
        
        TrueWord.transform.DOMove(Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0.7f,3f)), 0.3f);
        
        Vector3 rotateThisAngles = Quaternion.LookRotation(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.7f, 3f))-Camera.main.transform.position).eulerAngles;
        TrueWord.transform.DORotate(rotateThisAngles, 0.3f);

        float timeCounterForScale = 0;
        
        while (timeCounterForScale<=0.3f)
        {
            Vector3 a = Camera.main.ViewportToWorldPoint( new Vector3(1, 0, 3)) - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 3));

            if ( a.x < TWWordCreater.FirstLastLetterDistance  )//////////////////////////////////////
            {
                TrueWord.transform.localScale *= 0.95f;
                
            }

            else
            {
                timeCounterForScale += Time.deltaTime;
                yield return null;
            }

        }

        TrueWord.transform.localScale *= 0.85f;

         }
         else
         {
            TrueWord.transform.DOMoveY(TrueWord.transform.position.y + 8, 1.2f);

         }
        

        yield return new WaitForSeconds(0.8f);
        TrueWord.transform.DOScale(0, 0.4f);
       
        
        // TrueWord.transform.DOMove(Camera.main.transform.position + Camera.main.transform.forward * 3 - Camera.main.transform.up*3, 0.3f);
        
        //TrueWord.transform.DORotate(Camera.main.transform.rotation.eulerAngles, 0.3f);
        
        
        //yield return 
        
        /*
        float counter = 0;
        while (counter<=1)
        {
            counter += Time.deltaTime;
            TrueWord.transform.rotation = Quaternion.Lerp(TrueWord.transform.rotation,Camera.main.transform.rotation,Time.deltaTime*10 );
            TrueWord.transform.position = Vector3.Slerp(TrueWord.transform.position, Camera.main.transform.position-Camera.main.transform.forward*10  , Time.deltaTime);
            if(0.2f>counter && counter>0.15f)
            {
            yield return new WaitForSeconds(Time.deltaTime);
            }
            else
            {yield return null;
                
            }           

        }
        */
        
        
        
        yield return new WaitForSeconds(0.4f);
        SomeBreakingHeart();
        //TrueWord.transform.p




    }

    private void SomeBreakingHeart()
    {
        Destroy(TrueWord);
      //  Destroy(gameObject);
    }

    protected bool isItKickAndUnwantedLetter
    {
        get
        {
            return !isItKick  ||  !isItUnwantedLeter;
        }
    }
    
    
    
    
    

    protected virtual void ColliderParent()
    {
        
        
        SetOffCollider();
        SetOffMeshRenderer();
        

        ArrangementLetterPositionAfterCrush();
        GoExp.transform.parent = GameObject.FindGameObjectWithTag("Wheel").transform; // evlatliktan reddediliyor
        
        GoExp.SetActive(isItKickAndUnwantedLetter);
        
    }

    protected virtual void ColliderParentInCrushing()
    {
        
        SetOffCollider();
        GetComponent<MeshRenderer>().enabled = false;


        if (isItKickAndUnwantedLetter)
        {
           GoExp.transform.parent = GameObject.FindGameObjectWithTag("Wheel").transform; // evlatliktan reddediliyor
                                        
                                        GoExp.SetActive(true);
            
        }
        
        
    }

   

    protected virtual void ArrangementLetterPositionAfterCrush()
    {
        TWWordCreater.ArrangementLettersPositionAfterCrush();
    }


    protected virtual void ChangeColorPls(Color clrNew )
    {
        gameObject.GetComponent<MeshRenderer>().material.color = clrNew;
    }
    
    protected virtual void ColliderParentForce(bool willBeForced = true ,float aspectOfCollidePower=1f,float aspectOfUpForce =1f,bool isColorWhite =true)
    {
        GoExp.SetActive(true);
        
        for(int i =0;i<GoExp.transform.childCount;i++)
        {
            /* */
            
            Transform transChild = GoExp.transform.GetChild(i);
            GameObject goChild = transChild.gameObject;
            Rigidbody rbChild = goChild.GetComponent<Rigidbody>();
            
            //Rigidbody rbChild = GoExp.transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
            rbChild.useGravity = true;
            if (willBeForced)
            {
                MeshRenderer meshChild = goChild.GetComponent<MeshRenderer>();
                if(isColorWhite)
                { 
                    meshChild.material.color = Color.white;
                
                }
                else
                {
                    
                    meshChild.material.color = Color.red;
                    
                }
                rbChild.AddForce((-transChild.up+ Vector3.up*0.4f*aspectOfUpForce)*UnityEngine.Random.Range(750,950)*aspectOfCollidePower);
            }
                
                
                
                
            
        }
        
    }


    public virtual void crushing()
    {
        ColliderParentInCrushing();
        ColliderParentForce(true,0.3f,3,false);
        
        
        Invoke("SomeBreakingHeart",1.4f);
    }
    protected virtual void CurshMessageSent()
    {
        
        
        for (int i = 0; i < TrueWord.transform.childCount; i++)
        {
            if (TrueWord.transform.GetChild(i).gameObject.TryGetComponent(out UnwantedLetter unwanted))//(i == TWWordCreater.unwantedLetterIndex)
            { 
                unwanted.crushing();
                //TrueWord.transform.GetChild(i).gameObject.GetComponent<UnwantedLetter>().crushing();
            }
            
            /*
             // KULLANDIGIM BUTUN COMPONENTLER UNWANTEDLETTER DEN MIRAS ALDIGI ICIN UNWANTEDLETTER OLDUGU ZAMAN DIREKT VAR SAYIYIOR ACIKCASI
             
            else if (TrueWord.transform.GetChild(i).gameObject.TryGetComponent(out DesiredLetter desired))
            {
                //desired.crushing();
                //TrueWord.transform.GetChild(i).gameObject.GetComponent<DesiredLetter>().crushing();

            }
            else if (TrueWord.transform.GetChild(i).gameObject.TryGetComponent(out NotKickThatLetter notkick))
            {
               // notkick.crushing();
                //Debug.Log("lol");

            }
            
            
            */
            
                
        }
        
        
    }



    public void TriggeredDone()
    {
         TWWordCreater.isUnwantedTrigirred = true;

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //TriggeredDone();
            ColliderParent();       
            
            
            ColliderParentForce();
            
            StartCoroutine(afterTrueCrush());

            // Debug.Log(gameObject.name.Substring(0,1));
            //Debug.Log("Unwanted one is spotted");   
            //Destroy(gameObject);

        }

        
    }
    
    
    
    
    
    
    
    
    
}
