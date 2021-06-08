using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public SaveLoadSystem saveLoadSystem;
    public Player_Health PlayerHp;
    public Transform player;


    public void Awake()
    {
        if (PlayerPrefs.GetInt("LevelMove") == 1) //Если это переход с другой сцены, то загружаем данные из сейва, который был сделан до перехода на эту сцену
        {
            Continue(SceneManager.GetActiveScene().name);
        }
        else if (PlayerPrefs.GetInt("LoadHandleSave") == 1)
        {
            saveLoadSystem.LoadHandleSave();
            PlayerPrefs.SetInt("LoadHandleSave", 0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            saveLoadSystem.SaveData("Handle_Save");
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayerPrefs.SetInt("LoadHandleSave", 1);
            saveLoadSystem.LoadHandleSave();
        }
    }

    private void Continue(string nameSave)
    {
        PlayerPrefs.SetInt("QuickLoad", 1);
        saveLoadSystem.LoadData(nameSave);
        PlayerPrefs.SetInt("QuickLoad", 0);
        PlayerPrefs.SetInt("LevelMove", 0);
        Debug.Log("Loaded level exit save");
    }
}
