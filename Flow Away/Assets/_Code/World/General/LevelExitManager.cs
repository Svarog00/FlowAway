using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrustructure;

public class LevelExitManager : MonoBehaviour, ICoroutineRunner
{
    private const string PlayerTag = "Player";

    public string NextSceneName;

    [SerializeField] private UINoteTextScript _note;
    [SerializeField] private SaveManager _saveManager;

    private bool _readyToLeave = false;

    private IInputService _inputService;
    private SceneLoader _sceneLoader;
    private GameObject _player;

    private string _prevSceneName;

    private void Awake()
    {
        _sceneLoader = new SceneLoader(this);
    }

    private void Start()
    {
        _saveManager = FindObjectOfType<SaveManager>();
        _inputService = ServiceLocator.Container.Single<IInputService>();
    }

    private void Update()
    {
        if(!_readyToLeave)
        {
            return;
        }

        if (_inputService.IsInteractButtonDown())
        {
            MoveToNextScene();
        }
    }

    private void MoveToNextScene()
    {
        _saveManager.SaveGame(SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name); 
        //Создает файл сохранения с названием сцены, на которой игрок находится
        _prevSceneName = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
        _sceneLoader.Load(NextSceneName, OnLoaded, LoadSceneMode.Additive);
    }

    private void OnLoaded()
    {
        //Необходимо загрузить следующую и перенести персонажа на место перехода, отгрузить текущую сцену, 
        var exits = FindObjectsOfType<LevelExitManager>();
        foreach(var exit in exits)
        {
            if(exit.NextSceneName == _prevSceneName)
            {
                _player.transform.position = exit.transform.position;
                break;
            }
        }
        SceneManager.UnloadSceneAsync(_prevSceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Contains(PlayerTag))
        {
            _note.Appear("Press E to leave", 2f);
            _player = collision.gameObject;
            _readyToLeave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains(PlayerTag))
        {
            _note.Disappear(2f);
            _readyToLeave = false;
        }
    }
}
