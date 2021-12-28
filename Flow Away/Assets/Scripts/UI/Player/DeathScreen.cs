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
        PlayerHealthController playerHealthController = FindObjectOfType<PlayerHealthController>();
        playerHealthController.OnPlayerDeath += Player_OnDeath;
        visual.SetActive(false);
    }

    public void Player_OnDeath(object sender, EventArgs e)
    {
        visual.SetActive(true);
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("LoadHandleSave", 1);
        saveLoadSystem.LoadData("Handle_Save");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
