using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{

    public static void OnPause()
    {
        
    }

    public void Options()
    {
        
    }

    public void LoadGame()
    {
        
    }

    public void SaveGame()
    {
        
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        Application.Quit();
        #endif
        
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}
