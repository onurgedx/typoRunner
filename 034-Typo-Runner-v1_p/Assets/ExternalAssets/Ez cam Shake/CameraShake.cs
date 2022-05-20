using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
using EZCameraShake;
public class CameraShake : MonoBehaviour
{
   #region Singleton

   public static CameraShake Instance { get; private set; }
    
   private void Awake()
   {
   
      if (Instance != null && Instance != this)
      {
         Debug.Log("EXTRA : " + this + "  SCRIPT DETECTED RELATED GAME OBJ DESTROYED");
         Destroy(this.gameObject);
      }
      else
      {
         Instance = this;
      }
   }

   #endregion
   IEnumerator Shake(float duration, float magnitude)
   {
      Vector3 originalPos = transform.localPosition;

      float elapsed = 0f;

      while (elapsed<duration)
      {
         float x = Random.Range(-1f, 1f) * magnitude;
         float y = Random.Range(-1f, 1f) * magnitude;
         transform.localPosition = new Vector3(x, y, originalPos.z);
         elapsed += Time.deltaTime;
         yield return null;
      }

      transform.localPosition = originalPos;
   }

   public void CameraShakerStart()
   {
      StartCoroutine(Shake(1f,8f));
   }
}
