using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// day 7 used that
public class BigCreater4Words : MonoBehaviour
{

    
    
    public Transform  m_groundAmaUtangacTransform; 
    
    [Tooltip("Duration Creating Word ")]
    public float durationOfCreatingWord;
    
    [Tooltip("Put Word Prefab In")]
    public GameObject WordPrefab;

    
    [Tooltip("Put Wheel In That Has Wheel.cs script ")]
    public GameObject WheelGo;

    [Tooltip("Put Collider Of Wheel in")]
    public Collider m_ColliderOfWheel;
    
    [Tooltip("Set true if it is Player that We Control ")]
    public bool isForPlayer;
    
    [Tooltip("Put Image Component In From Player")]
    public Image m_ImageOfWord;
    
    //[Tooltip("List That Has Words Informations")]
    [Serializable]
    private class WordInformations
    {
        [Tooltip("Word as string")]
        public string _wordStr;
        
        [Tooltip("Index That Letter Unwanted ")]
        public int _unwantedIndex;
        
        [Tooltip("Type Kick If You Kick The Letter")]
        public string _typeOfWord;
        
        [Tooltip("Put Sprite In According Word")]
        public Sprite _sprite;

        //  public string _nameImage ;

    }

    [SerializeField] private List<WordInformations> WordInformationsList;

    
    
    // Olusturulan Word larin  wordCreater scriptinin listelendiği liste
    public List<WordCreater> ListWordCreaters;
    
    // Start is called before the first frame update
    void Start()
    {
       

        StartCoroutine(CreateWordObjects());
       
    }

    
   

    private IEnumerator CreateWordObjects()
    {
        transform.localPosition = Wheel.spawnPointOfWords;
        
        int counter=0;
        foreach (WordInformations i in WordInformationsList)
        {   
            
            counter++;
                        
            
            
            if (counter >1)
            {   
                createWord(i);
                yield return null;
                //yield return new WaitForSeconds(durationOfCreatingWord);
                
            }
            else if(counter <=1)
            {   
                createWord(i);
                yield return null;
                
               // WheelGo.transform.Rotate(Vector3.right,WheelGo.GetComponent<Wheel>().rotateSpeed*durationOfCreatingWord);
                

            }
            
            
            
            
            
            
        }

        for (int i = 0; i <ListWordCreaters.Count ; i++)
        {
            if (i < ListWordCreaters.Count - 1)
            {
                ListWordCreaters[i].nextWord = ListWordCreaters[i + 1].gameObject;
            }



        }
        
        

        float timeCounter = durationOfCreatingWord-5*Time.deltaTime;
         
        
        int counter2 = 0;
        while (counter2 < counter)
        {
            
            if (counter2>=1)
            {
                
                if(Wheel.isProceed)
                {
                    if (timeCounter >= durationOfCreatingWord  )
                    {
                        ListWordCreaters[counter2].setParentAndLocalScale(); 
                        counter2++;
                        timeCounter = 0;
                    }

                }
                
                    yield return null;
                    
            }
            else if (counter2 < 1)
            {
                ListWordCreaters[counter2].setParentAndLocalScale();
                yield return null;
                WheelGo.transform.Rotate(Vector3.right,Player.rotateSpeed*durationOfCreatingWord*Player.aspectOfRotateSpeed);
                counter2++;
            }
            timeCounter += Time.deltaTime*Player.aspectOfRotateSpeed;
            
            
            
        }
        
        
        
    }

    void setCamNull()
    {
        Camera.main.transform.parent =null;
    }

    void createWord(WordInformations i)
    {
        GameObject go = Instantiate(WordPrefab,transform); 
        WordCreater goWordCreater =go.GetComponent<WordCreater>();
        goWordCreater.wordStr = i._wordStr ; 
        goWordCreater.unwantedLetterIndex = i._unwantedIndex;
        goWordCreater.typeOfWord = i._typeOfWord;
        goWordCreater.m_sprite = i._sprite;
        goWordCreater.isForPlayer = isForPlayer;
        goWordCreater.m_ImageOfWord =  m_ImageOfWord;
        goWordCreater.CollOfWheel = m_ColliderOfWheel;
        goWordCreater.m_bigWordCreaterTransform = transform;
        goWordCreater.m_groundAmaUtangacTransform =m_groundAmaUtangacTransform ;
        
        ListWordCreaters.Add(goWordCreater);
        


    }


}
