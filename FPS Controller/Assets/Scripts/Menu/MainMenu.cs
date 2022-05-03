using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour{
<<<<<<< Updated upstream
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
=======

    public GameObject credit_video;

    public UnityEngine.Video.VideoPlayer credit_player;

    public VideoPlayer intro_player;

    public GameObject intro_video;


    void Awake(){
        Debug.Log("Video should be disabled!");
        credit_video.SetActive(false);
        intro_video.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if(credit_video.active){
            credit_player.Stop();
            credit_video.SetActive(false);
            }
            if(intro_video.active){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                intro_player.Stop();
                intro_video.SetActive(false);
            }
        }
    }

    public void StartTutorial(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //SceneManager.LoadScene("Tutorial");
    }

    public void doIntro(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void gotoCredits(){
        Debug.Log("Link credits scene!");
        //credit_video.SetActive(true);
        //SceneManager.LoadScene("Credits");
>>>>>>> Stashed changes
    }

    public void QuitGame(){
        Debug.Log("Quit button has been pressed!");
        Application.Quit();
    }
}
