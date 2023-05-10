using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrustructure;
using InventorySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour, ICoroutineRunner
{
    private const string HandleSaveName = "Handle_Save";

    private ISaveLoadService _saveLoadSystem;
    private ServiceLocator _serviceLocator;
    private SceneLoader _sceneLoader;

    #region PlayerComponents

    private Transform _playerPos;
    private PlayerHealthController _playerHealthController;
    private HealingCapsulesController _healingCapsulesController;
    private QuestValues _questValues;
    private InventoryRoot _inventory;

    #endregion

    private void Awake()
    {
        _serviceLocator = ServiceLocator.Container;
        _saveLoadSystem = _serviceLocator.Single<ISaveLoadService>();
        _sceneLoader = new SceneLoader(this);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame(HandleSaveName);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            LoadLastSave();
        }
    }

    public void SetPlayer(GameObject player)
    {
        _playerPos = player.transform;
        _playerHealthController = player.GetComponent<PlayerHealthController>();
        _healingCapsulesController = player.GetComponent<HealingCapsulesController>();
        _inventory = player.GetComponent<InventoryRoot>();
        _questValues = QuestValues.Instance;
    }

    public void ClearSaves()
    {
        _saveLoadSystem.ClearSaves();
    }

    public void SaveGame(string name)
    {
        WorldData worldData = new()
        {
            x = _playerPos.position.x,
            y = _playerPos.position.y,
            health = _playerHealthController.CurrentHealth,
            medkitCount = _healingCapsulesController.CapsulesCount,
            currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name,
            questValues = new List<QuestStage>(QuestValues.Instance.QuestList),
            items = new List<int>(_inventory.GetItemsIds())
        };

        _saveLoadSystem.SaveData(name, worldData);
    }

    public void LoadLastSave()
    {
        ApplyData(_saveLoadSystem.LoadHandleSave());
    }

    private void ApplyData(WorldData data)
    {
        if(data == null)
        {
            return;
        }    

        _playerPos.position = new Vector2(data.x, data.y);
        _playerHealthController.CurrentHealth = data.health;
        _healingCapsulesController.LoadCapsule(data.medkitCount);
        _questValues.QuestList = new List<QuestStage>(data.questValues);
        _inventory.LoadItems(data.items);

        var prevSceneName = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
        print(prevSceneName);
        _sceneLoader.Load(data.currentScene, null, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(prevSceneName);
    }
}
