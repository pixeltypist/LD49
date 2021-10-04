using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    Animator anim;
    public Button start, quit;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIdle()
    {
        anim.SetBool("isIdle", true);
    }

    public void StartGame()
    {
        //load scene
        SceneManager.LoadSceneAsync(1);
        //loading screen??
    }

    public void QuitGame()
    {
        print("Quitting game");
        Application.Quit(); 
    }
}
