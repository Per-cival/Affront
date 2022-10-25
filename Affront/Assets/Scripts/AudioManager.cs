using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   private static AudioManager _instance
   {
      get
      {
         if (_instance == null)
         {
            GameObject go = new GameObject();
            go.AddComponent<AudioManager>();
            return go.GetComponent<AudioManager>();
         }
         return _instance;
      }
      set => _instance = value;
   }

   private void Awake()
   {
      _instance = this;
   }
}
