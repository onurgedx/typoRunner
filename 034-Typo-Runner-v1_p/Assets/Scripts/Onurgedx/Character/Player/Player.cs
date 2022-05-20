using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// DAY 5 USED THIS
public class Player : MonoBehaviour
{
    
    
    protected GameObject rotaterGo;

    public float AspectOfPlayerSpeed = 4;

    public Animator playerAnimator;

    public  static float throwbackDuration = 1f;

    public float backDuration = 1f;
    
    public static Color colorwhite = Color.white;

    public static float rotateSpeed = -20;
    public static float aspectOfRotateSpeed = 1f;

    public Animator cameraAnimator; 
    
    // Start is called before the first frame update
    void Start()
    {
        if (cameraAnimator == null)
        {
            cameraAnimator = Camera.main.GetComponent<Animator>();
        }
        
        rotaterGo = GameObject.Find("/"+transform.parent.gameObject.name+ "/PlayerRotater");

       // playerAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovementProcess();
    }


    protected virtual string m_nameOfClass
    {
        get { return "Player"; }
    }

    protected virtual bool isItPlayer
    {
        get
        {
            return m_nameOfClass == "Player";
        }
        
    }

    protected virtual bool isItEnemy
    {
        get
        {
            return m_nameOfClass == "Enemy";
        }
    }

    
    
    protected virtual void PlayerMovementProcess()
    {
       
        transform.rotation = Quaternion.LookRotation(rotaterGo.transform.position-transform.position);
        transform.position = Vector3.Lerp(transform.position,rotaterPosSpecialX   , Time.fixedDeltaTime*AspectOfPlayerSpeed);
        


    }
    

    private Vector3 rotaterPosSpecialX
    {
        get
        {
            return new Vector3(rotaterGo.transform.position.x, transform.position.y,transform.position.z);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        // burasi patlicak 
        if ( other.gameObject.transform.parent.gameObject.tag=="word") //trigger degilse ve ust gameobjesinin tagi word ise 
        {
            if (other.gameObject.TryGetComponent(out NotKickThatLetter notkick))
            {
                //StartCoroutine(ThrowBack(-1));
                
            }
            else
            {
                StartCoroutine(ThrowBack());

            }
            


        
       
        
        }
    }

    public virtual IEnumerator ThrowBack(float backSpeed =-0.1f)
    {
        aspectOfRotateSpeedProperty = backSpeed;
        
        FallBack();
         
        yield return new WaitForSeconds(backDuration);

        aspectOfRotateSpeedProperty = 0.0f;
        yield return new WaitForSeconds(0.3f);
        aspectOfRotateSpeedProperty = 1;
        
        
        
    }

    public virtual float aspectOfRotateSpeedProperty
    {
        get
        {
            return aspectOfRotateSpeed;
        }

        set
        {
            aspectOfRotateSpeed = value;
        }
    }

    public virtual float rotateSpeedProperty
    {
        get
        {
            return rotateSpeed;
        }
        set
        {
            rotateSpeed = value;
        }
    }

    public void ThrowBackEksi1()
    {
        StartCoroutine(ThrowBack(-1));

    }
    
    public void Punchit()
    {       playerAnimator.SetTrigger("punch");

        
    }

    public void Kickit()
    {
        if (!playerAnimator.GetCurrentAnimatorStateInfo(2).IsName("StrikeForward"))
        {playerAnimator.SetTrigger("kick");

        }
        
        
    }
    public void FallBack()
    {
        playerAnimator.SetTrigger("fallback");
        if (isItPlayer)
        {
            CameraShakexd();
        }
    }

    public void CameraShakexd()
    {
        cameraAnimator.SetTrigger("shake");
    }
    
}
