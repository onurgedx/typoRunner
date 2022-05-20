using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Tools;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

// DAY 6 USED THIS
public class WordCreater : MonoBehaviour
{
    
    public bool isUnwantedTrigirred =false;
    
    public string wordStr;

    public int unwantedLetterIndex;
    public string typeOfWord;

    public GameObject m_DesiredWordText;
    public GameObject m_mainCanvas;
    public Image m_ImageOfWord;
    public Sprite m_sprite;
    public bool isForPlayer;
    
    public string wordStrExceptUnwanted;
    private float wordLength=0f;
    private float wordLengthWithoutUnwanted = 0f;


    public UnwantedLetter unwLetterScript;
    
    public float addedLength = 0f;
    public float addedLength2 = 0f;
    public Collider CollOfWheel;

  

    public bool oneTimeAccess = true;
    public bool oneTimeAccess2 = true;
    
    
    private MeshRenderer firstLetterMeshRenderer;
    private MeshRenderer lastLetterMeshRenderer;

    private EnemyController m_enemyController;
    private float xPosOfUnwantedNoKick;

    public int indexOfNewUnwantedIndex;

    public List<GameObject> ListLetterGameObjects;

    public Transform m_bigWordCreaterTransform;
    public Transform m_groundAmaUtangacTransform;

    public GameObject nextWord;
    
    public WordCreater(string wordStrInput)
    {
        this.wordStr = wordStrInput;
        
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        oneTimeAccess = true;
        oneTimeAccess2 = true;
    }

   
    private void Start()
    {
         // getmImageOfWord(); // gerek kalmadi direkt kurulurken aliniyor

         setCanvasAndDesiredWordText();
          AdjustChanceTruexPos();
       
         wordStrExceptUnwanted = wordStr.Remove(unwantedLetterIndex);
               
         
        
        //CollOfWheel = GameObject.Find("/" + transform.parent.parent.gameObject.name +"/Wheel/ground").GetComponent<Collider>();
       // CollOfWheel.gameObject.transform.parent.parent.GetChild(0).gameObject.TryGetComponent<EnemyController>(out EnemyController m_enemyController);
        
       CreateIt(); // creating letters
        
       
        SetFirstLastLetterMeshRenderer();

    }


    public void setTextDesiredWord()
    {
        
        m_DesiredWordText.GetComponent<Text>().text = wordStr4Canvas;
        //m_DesiredWordText.GetComponent<Text>().
        
        //m_DesiredWordText.GetComponent<RectTransform>().rect.width = m_DesiredWordText.GetComponent<Text>().minWidth;
        m_DesiredWordText.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        
        
    }

    public string wordStr4Canvas
    {
        get
        {
            if (typeOfWord == "kick")
            {
                return wordStr;

            }
            else
            {
                //string wordStrPunch = wordStr.Substring(0,unwantedLetterIndex) + wordStr.Substring(unwantedLetterIndex+1,wordStr.Length-unwantedLetterIndex)  ;
                string wordStrPunch = wordStr.Remove(unwantedLetterIndex,1);
                return wordStrPunch;

            }
            
        }
    }

    private void setCanvasAndDesiredWordText()
    {
        m_mainCanvas = GameObject.FindGameObjectWithTag("mainCanvas");
        m_DesiredWordText = GameObject.FindGameObjectWithTag("DesiredWordText");
        
    }


    private void AfterBeingCanvasEleman()
    {
         
        //ListLetterGameObjects
        
         // m_mainCanvas
         
    }

    public void setAdobted()
    {
        transform.parent = null;
    }
    

    private void OnDestroy()
    {
        unwLetterScript.DestroyKickAbles();
    }
    //
   

    private void AdjustChanceTruexPos()
    {
        indexOfNewUnwantedIndex = unwantedLetterIndex;
        
    }

    
    // Image componentini alir ve kullanir buraya ihtiyac yok artık!!!
    private void getmImageOfWord()
    {
        if (m_ImageOfWord == null)                                           
        {                                                                    
            m_ImageOfWord =   GameObject.Find("/" + transform.parent.parent.gameObject.name +"/Player/CanvasWorld0/Image/ImageOfWord").GetComponent<Image>();  
        }                                                                    
    }

    private void SetFirstLastLetterMeshRenderer()
    {
        firstLetterMeshRenderer = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        lastLetterMeshRenderer = transform.GetChild(wordStr.Length - 1).gameObject.GetComponent<MeshRenderer>();
        
    }

    private void Update()
    {



        SetImageIfShould();



    



    }

    

    public float FirstLastLetterDistance
    {
        get
        {
            //Debug.Log(transform.GetChild(wordStr.Length-1).position.x - transform.GetChild(0).position.x);

            float a = lastLetterMeshRenderer.bounds.max.x - firstLetterMeshRenderer.bounds.min.x; //transform.GetChild(wordStr.Length - 1).gameObject.GetComponent<MeshRenderer>().bounds.max.x - transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.min.x;

           return a; //transform.GetChild(wordStr.Length-1).position.x - transform.GetChild(0).position.x ;
        }
        
    }
    
    private IEnumerator ScaleSyncronize(float duzelmeDuration)//SetImageShould icinde calisiyor
    {
       // Vector3.Scale(transform.localScale , new Vector3(1 / 1.7f, 1 / 2f, 1f));
       // transform.localScale *= 3f;
       yield return new WaitForSeconds(1.5f);
        //transform.DOScale(transform.localScale / 2f, duzelmeDuration);
        transform.DOScale( Vector3.Scale(transform.localScale , new Vector3(1 / 1.7f, 1 / 2f, 1f)), duzelmeDuration);
        yield return new WaitForSeconds(duzelmeDuration);
        //transform.GetChild(unwantedLetterIndex).gameObject.GetComponent<UnwantedLetter>().KickIfThere();
        EnemyMakesChoise();
        
       // unwLetterScript.rotateAroundWheelList();
       // yield return new WaitForSeconds()
        

    }

    private void EnemyMakesChoise()
    {
        if (CollOfWheel.gameObject.transform.parent.parent.GetChild(0).gameObject
            .TryGetComponent<EnemyController>(out EnemyController m_enemyController))
        {
            MakeChanceToFalseChoiseForEnemies(m_enemyController);
                    
            //m_enemyController.xPosOfUnwantedLetter =transform.GetChild(indexOfNewUnwantedIndex).position.x; //enemyTrueXPosition;
                    
            //unwLetterScript.GoKick.transform.position.x
        }

    }
     private void MakeChanceToFalseChoiseForEnemies(EnemyController m_enemyController)
        {
            if (UnityEngine.Random.Range(0f, 1f) > 0.75f)// yanlis bilecekse // kucuk bir dogruluk payi icermektedir
            {
            
                if (typeOfWord =="kick" ) // yanlis tekme atacaksa // kucuk bir dogruluk payi icermektedir
                {
                    m_enemyController.xPosOfUnwantedLetter = unwLetterScript.Kickables[0].goKickable.transform.position.x;

                }
                else // yanlis yumruk atacaksa // kucuk bir dogruluk payi icermektedir
                {
                    indexOfNewUnwantedIndex = UnityEngine.Random.Range( 0 , wordStr.Length );
                
                    m_enemyController.xPosOfUnwantedLetter =transform.GetChild(indexOfNewUnwantedIndex).position.x;
                
                }
            
            
            }
            else// dogru bilecekse
            {
                if (typeOfWord == "kick") // dogru tekme atacaksa
                {
                    m_enemyController.xPosOfUnwantedLetter =unwLetterScript.GoKick.transform.position.x;

                }
                else  // dogru  yumruk atacaksa
                {
                    
                    m_enemyController.xPosOfUnwantedLetter =unwLetterScript.gameObject.transform.position.x;
                    
                }
                
            }
            
            
        }

    private void SetImageIfShould()
    {
        // transform.IsChildIOf(transforumun birisi)  : bana transformun altında mı oldugunu child i olup olmadigini soyluyor  
        //transform.GetSibilingIndex() bana transformun kacıncı indexteki childi oldugunu veriyor                              
        //// transform.IsChildOf(CollOfWheel.gameObject.transform)   //transform.parent.gameObject.tag == "ColliderOfWheel"    
        if (shouldBeImageActive && oneTimeAccess && Wheel.isProceed)
        {
            
          StartCoroutine( ScaleSyncronize( 0.4f));
         
            
            m_ImageOfWord.sprite = m_sprite;
            oneTimeAccess = false;

            EnemyMakesChoise();
            

        }
        
        
       // else if ( isItFirstSibiling && isItLastSibiling)
        //{
        //    Debug.Log("i am last one at BigWordCreater");
        //}
        
    }
    



    public void setInvisible()
    {
        foreach (GameObject go in  ListLetterGameObjects)
        {
            go.GetComponent<Renderer>().enabled = false;
            
        }

       
    }




    // askiya alindini
    public void SetKickAbleLetter(Vector3 posKickableLetter,string letterForLetter,float rotateAngleLetter,Quaternion qua)
    {
       
            unwLetterScript.CreatingAndSettingKickables2(posKickableLetter, letterForLetter, rotateAngleLetter,
                m_groundAmaUtangacTransform, 1, qua);
       



    }
    
    // 
    private bool isItFirstSibiling
    {
        get
        {
            return transform.GetSiblingIndex() == 0;
        }            
    }

    private bool isItSecondSibiling
    {
        get
        {
            return transform.GetSiblingIndex() == 1;
        }
    }

    private bool isItLastSibiling
    {
        get
        {
            return transform.GetSiblingIndex() == transform.parent.childCount-1;
        }
    }

    private bool isItChildOfground
    {
        get
        {
            return transform.IsChildOf(CollOfWheel.gameObject.transform);
        }
    }

  
    private bool shouldBeImageActive
    {
        get
        {
            return isItFirstSibiling && isItChildOfground;
        }
    }

    private bool shouldBeSmall
    {
        get
        {
            return isItSecondSibiling && isItChildOfground;
        }
    }
    
    public void colliderCenterChanging(Vector3 newCenter)
    {
        GetComponent<BoxCollider>().center = newCenter;
       // GetComponent<BoxCollider>().center = transform.InverseTransformPoint(newCenter);

    }

    void CreateIt()
    {
        MakewordStrUpper();


        CreateingLettersAndUpdatingwordLength();

        ArrangementLettersPosition();

        TransformsalDuzenlemeler();

        AddScript2Unwanted();

        AddScript2Desired();
        // day6 kaldı burda
        
    }


    private void AddLetterToListLetterGO(GameObject go)
    {
        ListLetterGameObjects.Add(go);
    }
    
    void MakewordStrUpper()
    {
        wordStr =  wordStr.ToUpper();
    }
    

    
    void CreateingLettersAndUpdatingwordLength()
    {
        
        
        foreach (char letter in wordStr)
        {
            
            
            GameObject go = Instantiate(GameObject.Find(letter.ToString()),transform);
            
            AddLetterToListLetterGO(go);// listeye ekler
            
            
            wordLength += go.GetComponent<Collider>().bounds.size.x;

         


        }
        
        
        
    }
    
    

     void ArrangementLettersPosition()
    {
        
        for(int i = 0 ; i< wordStr.Length;i++)
        {

            
            //listeden alsan kullanırdın iste aq
                GameObject go2 = transform.GetChild(i).gameObject;
            ListLetterGameObjects[i].transform.localPosition =
                new Vector3(addedLength + ListLetterGameObjects[i].GetComponent<Collider>().bounds.extents.x - wordLength / 2, 0, 0);
            addedLength += ListLetterGameObjects[i].GetComponent<Collider>().bounds.size.x;
            
            

        }

    }

     public void ArrangementLettersPositionAfterCrush()
     {
         if(isForPlayer)
         {
         //addedLength2 = 0;
         
         for (int i = 0; i < wordStr.Length; i++)
         {
             if(i != unwantedLetterIndex)
             {
                 wordLengthWithoutUnwanted +=ListLetterGameObjects[i].GetComponent<Collider>().bounds.size.x;
            
             }
         }

        
         for(int i = 0 ; i< wordStr.Length;i++)
         {
             if(ListLetterGameObjects[i].GetComponent<MeshRenderer>().enabled)
             {
                 
             GameObject go2 = ListLetterGameObjects[i];
             ListLetterGameObjects[i].transform.position =
                 new Vector3((Camera.main.transform.position.x - (wordLengthWithoutUnwanted / 2) + addedLength2 + go2.GetComponent<Collider>().bounds.extents.x )*1.2f, go2.transform.position.y, go2.transform.position.z);
             addedLength2 += go2.GetComponent<Collider>().bounds.size.x;
             go2.GetComponent<Collider>().enabled = false;
             
             }
            
         }

        
        }
     }



     void TransformsalDuzenlemeler()
    {
        transform.localPosition = Vector3.zero;  // Wheel.spawnPointOfWords; //new Vector3( CollOfWheel.bounds.center.x,CollOfWheel.bounds.min.y,CollOfWheel.bounds.center.z); // position belirleniyor
        
        transform.rotation = quaternion.LookRotation(-transform.up,transform.position-CollOfWheel.bounds.center);//Quaternion.Euler(-180,0,0); // rotasyon duzgun hale getiriliyor (-180 diyoruz ki karsimiza geldiginde duz gozuksun donup gelecek cunku)

        
        //setParentAndLocalScale();
        //transform.parent = GameObject.FindGameObjectWithTag("Wheel").transform; //tekerlekle donmesi icin child olarak duzenleniyor
        //transform.localScale *= 10; // burasi tam istedigim gibi degil daha otonom olmali  // bir tik ayriklik veriyor harfler arasinda ama daha profesyonel hale getirilebilir buraya donecegim !!!!

        
    }

    void AddScript2Unwanted()
    {
        // istenmeyen harfi aliyor
        // istenmeyen harfin colliderini trigger yapiyor
        // Istenmeyen harfe ozel component ekliyor
        GameObject go = ListLetterGameObjects[unwantedLetterIndex];
        go.GetComponent<Collider>().isTrigger = true;
       unwLetterScript = go.AddComponent<UnwantedLetter>();
       
       //go.GetComponent<MeshCollider>().
      

    }

    void AddScript2Desired()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != unwantedLetterIndex)
            {
               
                ListLetterGameObjects[i].AddComponent<DesiredLetter>();
            }
            

        }
        

    }

    public void setParentAndLocalScale()
    {
        transform.parent = CollOfWheel.transform; //GameObject.FindGameObjectWithTag("Wheel").transform; //tekerlekle donmesi icin child olarak duzenleniyor
        
        transform.localScale *= 10; // burasi tam istedigim gibi degil daha otonom olmali  // bir tik ayriklik veriyor harfler arasinda ama daha profesyonel hale getirilebilir buraya donecegim !!!!
      //  DuplicateSelf(5);
      
      
      //transform.GetChild(unwantedLetterIndex).gameObject.GetComponent<UnwantedLetter>().KickIfThere();
      
      
      
      //ScaleSyncronize( 5f);
    // StartCoroutine( ScaleSyncronize( 0.51f));  // sirasi gelince kuculuyor
      
     // ListLetterGameObjects[unwantedLetterIndex].GetComponent<UnwantedLetter>().KickIfThere();
     unwLetterScript.rotateAroundWheelList();
    
     unwLetterScript.GetOutOfParentToBigParentFORKickables();

     transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1.7f, 2f, 1f));
     //transform.localScale *= 2;

    }
    
    


    
    
}
