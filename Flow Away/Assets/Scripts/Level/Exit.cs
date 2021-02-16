using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Text note;
    public string nextSceneName;
    public SaveLoadSystem sls;
    private bool _readyToLeave = false;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _readyToLeave)
        {
            sls.SaveData(SceneManager.GetActiveScene().name); //Создает файл сохранения с названием сцены, на которой игрок находится
            PlayerPrefs.SetInt("LevelMove", 1); //Устанавливается флаг перехода 
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single); //Загружается следующая сцена
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            note.CrossFadeAlpha(1, 2f, false);
            note.text = "Press E to move";
            _readyToLeave = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            note.CrossFadeAlpha(0, 2f, false);
            _readyToLeave = false;
        }
    }
}
