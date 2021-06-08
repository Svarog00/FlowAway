using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    SaveLoadSystem saveLoadSystem;

    private void Start()
    {
        saveLoadSystem = gameObject.AddComponent<SaveLoadSystem>();
    }

    public void Play()
    {
        saveLoadSystem.ClearSaves();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); //start first lvl
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("LoadHandleSave", 1);
        saveLoadSystem.LoadHandleSave();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
