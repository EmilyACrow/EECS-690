using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseUI;

    public GameObject optionsUI;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          if (isPaused)
          {
            Resume();
          }
          else
          {
            Pause();
          }
        }
        /*
        if (Input.GetKeyDown("q"))
        {
          if (isPaused)
          {
            gotoMenu();
          }
        }
        if (Input.GetKeyDown("s"))
        {
          if (isPaused)
          {
            save();
          }
        }
        */
    }

    public void Resume ()
    {
      pauseUI.SetActive(false);
      Time.timeScale = 1f;
      isPaused = false;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    void Pause ()
    {
      pauseUI.SetActive(true);
      Time.timeScale = 0f;
      isPaused = true;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }

    public void gotoOptions()
    {
      Debug.Log("Loading Options!");
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = true;
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
