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
        PlayerPrefs.DeleteAll();
        if (Directory.Exists(Application.dataPath + "/Saves"))
        {
            var dirInfo = new DirectoryInfo(Application.dataPath + "/Saves");
            foreach (var file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }
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
