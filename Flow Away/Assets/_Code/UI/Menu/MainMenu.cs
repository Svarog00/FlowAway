using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string InitialSceneName = "Initial";

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Play()
    {
        PlayerPrefs.SetInt("StartNewGame", 1);
        SceneManager.LoadSceneAsync(InitialSceneName); //start first lvl
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("LoadHandleSave", 1);
        SceneManager.LoadSceneAsync(InitialSceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
