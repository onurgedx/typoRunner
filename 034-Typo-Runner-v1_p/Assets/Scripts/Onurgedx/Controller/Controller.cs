using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DAY 2 USED THAT 
public class Controller : MonoBehaviour
{
    private Vector3 LastPositionOfMouse;

    public GameObject playerGo;

    public GameObject rotaterGo;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log(transform.parent.gameObject.name);
        if (playerGo == null) {playerGo = GameObject.Find("/"+transform.parent.gameObject.name+"/Player");        }
        
        if(rotaterGo == null){rotaterGo = GameObject.Find("/"+transform.parent.gameObject.name+"/PlayerRotater"); }
         
       
         //  playerGo = GameObject.Find("Player");
      //   rotaterGo = GameObject.FindGameObjectWithTag("PlayerRotater");
    }

    // Update is called once per frame
    void FixedUpdate()                        
    {
        
        InputProcessFromController();
        
        
        

    }

    protected  virtual void InputProcessFromController()
    {
        if ( touchDown0)
        { 
            Debug.Log(transform.GetSiblingIndex());
            AdjustLastPositionOfMouse();
            
        }
        else if(isTouch0)
        {

            // Debug.Log(AmountMouseMovementAxisX);
            
            rotaterGo.GetComponent<Rotater>().MovePlayer(AmountMouseMovementX);
            
            AdjustLastPositionOfMouse();
        
        }

    }


    private bool touchDown0
    {
        get
        {
//#if UNITY_ANDROID
//            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
//#elif UNITY_EDITOR
            return Input.GetMouseButtonDown(0);
//#endif

        }
    }


    private bool isTouch0
    {        get
        {
//#if UNITY_ANDROID
//            return Input.touchCount > 0 ;
//#elif UNITY_EDITOR
            return Input.GetMouseButton(0);
//#endif

        }

        
    }

    private void AdjustLastPositionOfMouse()
    { 
        //Mouse0 a basiliyken  girer ve last position u gunceller 

        LastPositionOfMouse = ViewportPointOfMousePosition;


    }

    //viewportPoint olarak verir bana mouse kordinatini 
    private Vector3 ViewportPointOfMousePosition
    {
        get
        {
//#if UNITY_ANDROID
//          return  Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
//#elif UNITY_EDITOR
        return  Camera.main.ScreenToViewportPoint( Input.mousePosition);
//#endif
        }
    }

    private Vector3 DistanceOfMouseMovement
    {
        get
        {
            return (ViewportPointOfMousePosition - LastPositionOfMouse);
        }
    }
    
    // NE KADAR POSTION DEGISTIRDIGINI VECTOR3 OLARAK DONDURUR VE SADECE X DEGERI 0DAKLIDIR
    private Vector3 AmountMouseMovementAxisX
    {
        get
        {
            Vector3 rotateAxisIfWheelProceed = Wheel.isProceed ? Vector3.right : Vector3.zero;
            return Vector3.Scale(DistanceOfMouseMovement,rotateAxisIfWheelProceed);
        }
    }

    private float AmountMouseMovementX
    {
        //Belli bir degerde dönmesi icin 250 degerini belirledim.
        // 250 degeri degistirilebilir. 
        get
        {
            return AmountMouseMovementAxisX.x;
            //  return  AmountMouseMovementAxisX.x > 250 ? 1 : AmountMouseMovementAxisX.x/250 ;

        }
    }
    
    
}
