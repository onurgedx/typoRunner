using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    public float enemySpeed;
    public Transform enemyPivot;
    public float aspectOfRotateEnemy4Property;

    public float totalDegree = 0;

    public override IEnumerator ThrowBack(float backSpeed = -0.1f)
    {
        Debug.Log("loooool");
        aspectOfRotateSpeedProperty = backSpeed;

        FallBack();

        yield return new WaitForSeconds(backDuration);

        aspectOfRotateSpeedProperty = 0.0f;
        yield return new WaitForSeconds(0.3f);
        aspectOfRotateSpeedProperty = 1;



    }

    protected override string m_nameOfClass { get {return "Enemy"; } }


private void RunReally()
    {
        float degreeDiffRotate= (rotateSpeed*aspectOfRotateSpeed -aspectOfRotateSpeedProperty*rotateSpeedProperty  )*Time.fixedDeltaTime;
        totalDegree += degreeDiffRotate;
        if (Mathf.Abs(totalDegree) < 60)
        {
            transform.RotateAround(enemyPivot.position,Vector3.right,  degreeDiffRotate);
            rotaterGo.transform.RotateAround(enemyPivot.position,Vector3.right,  degreeDiffRotate);
        }
        
        
    }


    
    
    public override float aspectOfRotateSpeedProperty
    {
        get
        {
            return aspectOfRotateEnemy4Property;
        }
        set
        {
            aspectOfRotateEnemy4Property = value;
        }
    }

    public override float rotateSpeedProperty
    {
        get
        {
            return enemySpeed;
        }
        set
        {
            enemySpeed = value;
        }
    }

    protected override void PlayerMovementProcess()
    {
        base.PlayerMovementProcess();
        RunReally();
    }
    
    
    
    
}
