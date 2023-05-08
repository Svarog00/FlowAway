using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Services;
using InventorySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private const string HandleSaveName = "Handle_Save";
    private const string QuickLoadSaveName = "QuickLoad";
    private const string LoadHandleSaveName = "LoadHandleSave";
    private const string LevelMoveName = "LevelMove";

    private ISaveLoadService _saveLoadSystem;
    private ServiceLocator _serviceLocator;

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
    }

    private void Start()
    {
        //Флаг LevelMove ставится в Exit.cs при переходе со сцены на сцену
        /*if (PlayerPrefs.GetInt(LevelMoveName) == 1) //Если это переход с другой сцены, то загружаем данные из сейва, который был сделан до перехода на эту сцену
        {
            Continue(SceneManager.GetActiveScene().name);
        }
        else if (PlayerPrefs.GetInt(LoadHandleSaveName) == 1)
        {
            LoadLastSave();
            PlayerPrefs.SetInt(LoadHandleSaveName, 0);
        }*/
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame(HandleSaveName);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayerPrefs.SetInt(LoadHandleSaveName, 1);
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

    private void Continue(string nameSave)
    {
        PlayerPrefs.SetInt(QuickLoadSaveName, 1);
        ApplyData(_saveLoadSystem.LoadData(nameSave));
        PlayerPrefs.SetInt(QuickLoadSaveName, 0);
        PlayerPrefs.SetInt(LevelMoveName, 0);
    }

    private void LoadLastSave()
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
    }
}
