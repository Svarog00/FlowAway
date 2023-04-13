using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject inGameMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(isPaused)
            {
                case false:
                    {
                        Time.timeScale = 0;
                        isPaused = true;
                        inGameMenu.SetActive(isPaused);
                        break;
                    }
                case true:
                    {
                        Time.timeScale = 1;
                        isPaused = false;
                        inGameMenu.SetActive(isPaused);
                        break;
                    }
            }
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        isPaused = false; 
        inGameMenu.SetActive(false);
    }

    public void Options()
    {
        //In progress...
    }

    public void Menu()
    {
        //saving
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void Exit()
    {
        Application.Quit();
    }
}
