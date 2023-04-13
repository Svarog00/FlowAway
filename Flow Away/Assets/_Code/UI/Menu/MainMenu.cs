using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    private SaveManager _saveManager;

    private void Start()
    {
        _saveManager = gameObject.AddComponent<SaveManager>();
    }

    public void Play()
    {
        _saveManager.ClearSaves();
        QuestValues.Instance?.Clear();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); //start first lvl
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("LoadHandleSave", 1);
        _saveManager.LoadLastSave();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
