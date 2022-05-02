using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject winUI;

    public static bool isWin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.m_totalNodes == 5){
            isWin = true;
            displayWin();
            PlayerController.m_totalNodes = 0;
        }
    }

    void displayWin()
    {
      winUI.SetActive(true);
      Time.timeScale = 0f;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
}
