using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.Infrastructure.Services;

public class Exit : MonoBehaviour
{
    public string NextSceneName;

    [SerializeField] private UINoteTextScript _note;
    [SerializeField] private SaveManager _saveManager;

    private bool _readyToLeave = false;
    private IInputService _inputService;

    private void Start()
    {
        _inputService = ServiceLocator.Container.Single<IInputService>();
    }

    private void Update()
    {
        if (_inputService.IsInteractButtonDown() && _readyToLeave)
        {
            _saveManager.SaveGame(SceneManager.GetActiveScene().name); //Создает файл сохранения с названием сцены, на которой игрок находится
            PlayerPrefs.SetInt("LevelMove", 1); //Устанавливается флаг перехода 
            SceneManager.LoadSceneAsync(NextSceneName, LoadSceneMode.Single); //Загружается следующая сцена
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerControl>())
        {
            _note.Appear("Press E to leave", 2f);
            _readyToLeave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>())
        {
            _note.Disappear(2f);
            _readyToLeave = false;
        }
    }
}
