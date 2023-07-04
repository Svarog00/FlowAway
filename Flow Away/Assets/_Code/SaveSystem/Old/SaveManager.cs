using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrustructure;
using InventorySystem;
using System.Collections.Generic;
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
    private GadgetManager _gadgetManager;

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
        _gadgetManager = player.GetComponent<GadgetManager>();
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
            Health = _playerHealthController.CurrentHealth,
            MedkitCount = _healingCapsulesController.CapsulesCount,
            CurrentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name,
            QuestValues = new List<QuestStage>(QuestValues.Instance.QuestList),
            Items = new List<int>(_inventory.GetItemsIds()),
            AbilityStatuses = new List<bool>(_gadgetManager.GetAbilitiesStatus()),
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
        _playerHealthController.CurrentHealth = data.Health;
        _healingCapsulesController.LoadCapsule(data.MedkitCount);
        _questValues.QuestList = new List<QuestStage>(data.QuestValues);
        _inventory.LoadItems(data.Items);
        _gadgetManager.SetGadgetsStatus(data.AbilityStatuses);

        var prevSceneName = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
        print(prevSceneName);
        _sceneLoader.Load(data.CurrentScene, null, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(prevSceneName);
    }
}
