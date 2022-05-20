using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    public GameObject refRotateGo;

    public float xPosOfUnwantedLetter; 
    // Start is called before the first frame update
    void Start()
    {
        refRotateGo.GetComponent<Rotater>().DirectSetPosition(xPosOfUnwantedLetter);
    }

    // Update is called once per frame
    void Update()
    {
        
            // burdan surekli getcomponent diye alıyor aq bunu oyun basinda alsaydın direkt rotater scripti diye ohhh kullanirdin yarin bunu da duzelt   
        refRotateGo.GetComponent<Rotater>().DirectSetPosition(xPosOfUnwantedLetter);
        
        
        
    }
    
    
    
    
}
