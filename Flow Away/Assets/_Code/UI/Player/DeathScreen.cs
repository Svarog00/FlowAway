using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private SaveLoadService _saveLoadSystem;
    [SerializeField] private GameObject _visual;

    private void Start()
    {
        var playerHealthController = FindObjectOfType<PlayerHealthController>();
        playerHealthController.OnPlayerDeath += Player_OnDeath;
        _visual.SetActive(false);
    }

    public void Player_OnDeath(object sender, EventArgs e)
    {
        _visual.SetActive(true);
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("LoadHandleSave", 1);
        _saveLoadSystem.LoadData("Handle_Save");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
