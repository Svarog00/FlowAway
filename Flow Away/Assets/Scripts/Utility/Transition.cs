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
        if (PlayerPrefs.GetInt("LevelMove") == 1)
        {
            Continue(SceneManager.GetActiveScene().name);
        }
    }

    void Start()
    {
        saveLoadSystem.SaveData("CheckPoint");
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
