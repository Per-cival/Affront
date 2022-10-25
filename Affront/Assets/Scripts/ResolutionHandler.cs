using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Subsystems;

public class ResolutionHandler : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { ResolutionSetter(); });
    }

    private void ResolutionSetter()
    {
        switch (dropdown.value)
        {
            case 0 : Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
                break;
            case 1 : Screen.SetResolution(1280, 720, Screen.fullScreenMode);
                break;
            case 2 : Screen.SetResolution(720, 480, Screen.fullScreenMode);
                break;
            default: Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
                break;
        }
    }
    
}
