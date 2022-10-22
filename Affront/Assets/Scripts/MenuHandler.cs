using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    private GameObject pauseGameObject;
    private Canvas PauseCanvas;
    private Canvas OptionsCanvas;
    private void Awake()
    {
        pauseGameObject = GameObject.Find("PauseGameObject");
        PauseCanvas = pauseGameObject.transform.GetChild(0).GetComponent<Canvas>();
        OptionsCanvas = pauseGameObject.transform.GetChild(1).GetComponent<Canvas>();


    }

    public void OnPause()
    {
        if (!OptionsCanvas.gameObject.activeSelf) //if options menu is not enabled, turn on pause menu.
        {
            PauseCanvas.gameObject.SetActive(!PauseCanvas.gameObject.activeSelf);
            Time.timeScale = PauseCanvas.gameObject.activeSelf ? 0 : 1;
        }
        else
        {
            OptionsCanvas.gameObject.SetActive(false);
            PauseCanvas.gameObject.SetActive(true);
        } 
        
        
        
        

    }

    public void Options()
    {
        OptionsCanvas.gameObject.SetActive(true);
        PauseCanvas.gameObject.SetActive(false);        
    }
    
        public void LoadGame()
    {
        
    }

    public void Back()
    {
        OptionsCanvas.gameObject.SetActive(false);
        PauseCanvas.gameObject.SetActive(true);

    }
    public void SaveGame()
    {
        
    }

    public void QuitToMenu()
    {
        //call load, parameter of mainmenu scene
    }

    public void QuitGame() //quit and quittomainmenu
    {
        #if UNITY_EDITOR
        Application.Quit();
        #endif
        
        System.Diagnostics.Process.GetCurrentProcess().Kill(); //I don't like either of these solutions.
        //Application.Quit doesn't work for some reason, and killing current process is sudden and jarring.
    }
}
