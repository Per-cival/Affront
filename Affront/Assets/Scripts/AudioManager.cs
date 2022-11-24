using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AudioManager : MonoBehaviour
{
   private static AudioManager _instance;
   public AudioClip[] audioClips;
   private AudioSource audioSource;
   public static AudioManager audioManager
   {
      get
      {
         if (_instance == null)
         {
            GameObject audManager = new GameObject("AudioManager", typeof(AudioManager), typeof(AudioSource));
            return audManager.GetComponent<AudioManager>();
         }
         return _instance;
      }
   }


   private void Awake()
   {
      _instance = this;
      DontDestroyOnLoad(this);

      audioClips = Resources.LoadAll<AudioClip>("Assets/Resources/Audio/Ambient");
      audioSource = GetComponent<AudioSource>();



   }

   public void PlaySound(string sound, bool loop)
   {
      foreach (AudioClip clip in audioClips) //wrap in a static function called anywhere
      {
         if (clip.name == sound)
         {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
         }
      }

   }
}
