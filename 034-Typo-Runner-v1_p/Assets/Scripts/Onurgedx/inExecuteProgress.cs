using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class inExecuteProgress : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            Transform goTransform = transform.GetChild(i);
            for (int k=0; k < goTransform.childCount; k++)
            {
                Rigidbody rg = goTransform.GetChild(k).gameObject.GetComponent<Rigidbody>();
                rg.mass = 1.51f;
                rg.drag = 0.93f;
                rg.angularDrag = 0.71f;
            }
        }

    }
}
