using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private SaveManager _saveLoadSystem;
    [SerializeField] private GameObject _visual;

    private void Start()
    {
        var playerHealthController = FindObjectOfType<PlayerHealthController>();
        playerHealthController.OnPlayerDeath += Player_OnDeath;
        _saveLoadSystem = FindObjectOfType<SaveManager>();

        _visual.SetActive(false);
    }

    public void Player_OnDeath(object sender, EventArgs e)
    {
        _visual.SetActive(true);
    }

    public void Retry()
    {
        _saveLoadSystem.LoadLastSave();
        _visual.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
