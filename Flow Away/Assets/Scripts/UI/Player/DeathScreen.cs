using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public SaveLoadSystem saveLoadSystem;
    public GameObject visual;

    private void Start()
    {
        Player_Health player = FindObjectOfType<Player_Health>();
        player.OnDeath += Player_OnDeath;
        visual.SetActive(false);
    }

    private void Player_OnDeath(object sender, EventArgs e)
    {
        visual.SetActive(true);
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("LoadHandleSave", 1);
        saveLoadSystem.LoadData("Handle_Save");
        /*player.SetActive(true);
        PlayerPrefs.SetInt("QuickLoad", 0);
        visual.SetActive(false);*/
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
