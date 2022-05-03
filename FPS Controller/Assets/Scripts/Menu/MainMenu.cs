using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public GameObject credit_video;

    public UnityEngine.Video.VideoPlayer credit_player;

    void Awake(){
        Debug.Log("Video should be disabled!");
        credit_video.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            credit_player.Stop();
            credit_video.SetActive(false);
        }
    }

    public void StartTutorial(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //SceneManager.LoadScene("Tutorial");
    }
    
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("FPS Controller Test");
    }

    public void gotoCredits(){
        Debug.Log("Link credits scene!");
        //credit_video.SetActive(true);
        //SceneManager.LoadScene("Credits");
    }

    public void QuitGame(){
        Debug.Log("Quit button has been pressed!");
        Application.Quit();
    }
}
