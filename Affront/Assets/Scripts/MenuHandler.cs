using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    private GameObject pauseGameObject;
    private Canvas PauseCanvas;
    private Canvas OptionsCanvas;
    private void Awake()
    {
        pauseGameObject = GameObject.Find("MenuGameObject");
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

    public void Continue()
    {
        //Load save file
    }

    public void NewGame()
    {
        SceneManager.UnloadSceneAsync((int) Level.MainMenu);
        SceneManager.LoadSceneAsync((int) Level.Forest, LoadSceneMode.Single);

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
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync((int) Level.MainMenu, LoadSceneMode.Single);

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
