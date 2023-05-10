using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private const string MainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    [SerializeField] private GameObject _visual;

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
                        _visual.SetActive(isPaused);
                        break;
                    }
                case true:
                    {
                        Time.timeScale = 1;
                        isPaused = false;
                        _visual.SetActive(isPaused);
                        break;
                    }
            }
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        isPaused = false; 
        _visual.SetActive(false);
    }

    public void Options()
    {
        //In progress...
    }

    public void Menu()
    {
        //saving
        Time.timeScale = 1;
        SceneManager.LoadScene(MainMenuSceneName);

    }

    public void Exit()
    {
        Application.Quit();
    }
}
