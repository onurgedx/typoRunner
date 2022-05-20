using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  DAY 4 USED THAT
public class Wheel : MonoBehaviour
{

    public static Vector3 spawnPointOfWords;
    
    
        // Start is called before the first frame update
    void Awake()
    {
        string LikeWorldName ="LikeWorld"  ; //transform.parent.gameObject.name;
        MeshRenderer CollOfWheel = GameObject.Find("/"+LikeWorldName+"/Wheel/ground").GetComponent<MeshRenderer>();
        //spawnPointOfWords =  new Vector3( CollOfWheel.bounds.center.x,CollOfWheel.bounds.min.y,CollOfWheel.bounds.center.z);
        spawnPointOfWords =  new Vector3( CollOfWheel.bounds.center.x,CollOfWheel.bounds.center.y,CollOfWheel.bounds.max.z);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateWheel();
        
    }


    

    private void RotateWheel() {


        
        
            // tekerlegi hareket ettirir. rotateSpeed : saniyedeki donecegi aci
            // make the wheel be rotated according to rotateSpeed that angle per second
            transform.Rotate(Vector3.right,Player.aspectOfRotateSpeed*Player.rotateSpeed*Time.fixedDeltaTime);
                    
        
        
    }


    public static bool isProceed
    {
        get { return Player.aspectOfRotateSpeed > 0; }
    }

    public static bool isRegress
    {
        get { return Player.aspectOfRotateSpeed < 0; }
    }

    public static bool isStopped
    {
        get
        {
            return Player.aspectOfRotateSpeed == 0; 
            
        }
    }
    
}
