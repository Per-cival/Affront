using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public void OnPause()
    {
        GameObject pauseGameObject = GameObject.Find("PauseGameObject");
        Canvas PauseCanvas = pauseGameObject.transform.GetChild(0).GetComponent<Canvas>();
        
        PauseCanvas.gameObject.SetActive(!PauseCanvas.gameObject.activeSelf);

        Time.timeScale = PauseCanvas.gameObject.activeSelf ? 0 : 1;

        #if UNITY_STANDALONE
        Cursor.visible = !Cursor.visible;
        #endif

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
