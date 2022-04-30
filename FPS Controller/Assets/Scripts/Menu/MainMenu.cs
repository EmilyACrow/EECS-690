using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    public void StartTutorial(){
        SceneManager.LoadScene("Tutorial");
    }
    
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("FPS Controller Test");
    }

    public void gotoCredits(){
        Debug.Log("Link credits scene!");
        //SceneManager.LoadScene("Credits");
    }

    public void QuitGame(){
        Debug.Log("Quit button has been pressed!");
        Application.Quit();
    }
}
