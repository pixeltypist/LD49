using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject menu;
    public Button Quit, Continue;
    public GameEvent PauseGame, UnPause;
    bool menuActive;

    private void Start() {
        menu.SetActive(false);
        menuActive = false;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!menuActive)
            {
                 menu.SetActive(true);
                print("Trying to turn on menu");
                menuActive = true;
                PauseGame.Raise();
            }   
            else
            {
                menuActive = false;
                UnPause.Raise();
                menu.SetActive(false);
            }
            
        }
        
    }

    public void ContinueGame()
    {
        menuActive = false;
        UnPause.Raise();
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        print("Quitting game");
        Application.Quit();
    }
}
