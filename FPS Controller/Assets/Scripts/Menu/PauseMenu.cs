using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isOptioned = false;

    public GameObject pauseUI;

    public GameObject optionsUI;

    public void setOptioned(){
      isOptioned = true;
    }

    public void setOptionedFalse(){
      isOptioned = false;
    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOptioned == false)
        {
          if (isPaused == true)
          {
            Resume();
          }
          else
          {
            Pause();
          }
        }
    }

    public void Resume ()
    {
      pauseUI.SetActive(false);
      Time.timeScale = 1f;
      isPaused = false;
      isOptioned = false;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    public void Quit ()
    {
      Application.Quit();
    }

    void Pause ()
    {
      pauseUI.SetActive(true);
      Time.timeScale = 0f;
      isPaused = true;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }

    public void gotoPause()
    {
      Debug.Log("Loading Pause!");
        pauseUI.SetActive(true);
        optionsUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = true;
        isOptioned = false;
    }

    public void gotoOptions()
    {
      Debug.Log("Loading Options!");
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = true;
        isOptioned = true;
    }

    public void gotoMenu()
    {
      Debug.Log("Loading menu!");
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
      SceneManager.LoadScene("Menu");
    }
}
