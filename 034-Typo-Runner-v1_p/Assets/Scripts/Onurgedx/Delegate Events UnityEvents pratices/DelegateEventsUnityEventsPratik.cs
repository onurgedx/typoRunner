using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelegateEventsUnityEventsPratik : MonoBehaviour
{
    
    
    // Unity Event tanimlanmasi
    //[System.Serializable] public class SayiUnityEvent : UnityEvent<int> {}
    [System.Serializable] public class SayiUnityEvent : UnityEvent {}

    public SayiUnityEvent unityEventObjesi;



    delegate void MyDelegate();

    private  MyDelegate attack;
    
    
    // Start is called before the first frame update
    void Start()
    {
        unityEventObjesi.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            attack = PrimaryAttack;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            attack = SecondaryAttack;
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            // attack i kullanirken  null olup olmadigina dikkat etmemiz gerekmektedir.
            // direkt if ile null degilse diye kullanabiliriz 
            // veya attack?.Invoke() seklinde de kullanabiliriz
            /*
            if (attack != null)
            {
                attack();
            } 
            */
            
            
            // bunu su sekilde de kullanabiliriz
             attack?.Invoke();
            
        }
        
    }
    
        
        
    
    void PrimaryAttack()
    {
        Debug.Log("Primary Attack");
        // Primary Attack
    }

    void SecondaryAttack()
    {
        Debug.Log("Secondary Attack");
        // Secondary Attack
    }

    public void printUE2()
    {
        Debug.Log("UE2");
    }

    public void printUE3()
    {
        Debug.Log("UE3");
    }






}
