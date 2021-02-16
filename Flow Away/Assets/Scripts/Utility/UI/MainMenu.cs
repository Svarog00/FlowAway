using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    SaveLoadSystem saveLoadSystem;

    private void Start()
    {
        saveLoadSystem = gameObject.AddComponent<SaveLoadSystem>();
    }

    public void Play()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //start first lvl
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("LoadSave", 1);
        saveLoadSystem.LoadData("CheckPoint");// в SaveLoadSystem.cs
    }

    public void Exit()
    {
        Application.Quit();
    }
}
