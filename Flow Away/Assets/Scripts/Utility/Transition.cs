using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
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
        if(PlayerPrefs.GetInt("LoadSave") == 1)
        {
            Continue("CheckPoint");
            PlayerPrefs.SetInt("LoadSave", 0);
        }    
    }

    void Start()
    {
        saveLoadSystem.SaveData("CheckPoint"); //Создание чекпоинта для продолжения игры из главного меню.
    }

    private void Continue(string nameSave)
    {
        PlayerPrefs.SetInt("QuickLoad", 1);
        saveLoadSystem.LoadData(nameSave);
        PlayerPrefs.SetInt("QuickLoad", 0);
        PlayerPrefs.SetInt("LevelMove", 0);
        Debug.Log("Load save");
    }
}
