using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrustructure;
using InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class LoadPlayerSceneState : IState
    {
        private const string PlayerSceneName = "PlayerLayer";
        private const string NewGameStartLevelName = "Graveyard";
        private const string StartNewGamePlayerPrefsKey = "StartNewGame";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly ISaveLoadService _saveLoadService;

        public LoadPlayerSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _sceneLoader.Load(PlayerSceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            GameObject hero = _gameFactory.CreateHero();
            _gameFactory.CreateHud();

            CameraFollow(hero);

            var inventoryUI = Object.FindObjectOfType<UI_Inventory>();
            inventoryUI.SetInventory(hero.GetComponent<InventoryRoot>());

            var saveManager = Object.FindAnyObjectByType<SaveManager>();
            saveManager.SetPlayer(hero);

            if (PlayerPrefs.GetInt(StartNewGamePlayerPrefsKey) == 1)
            {
                _stateMachine.Enter<LoadLevelState, string>(NewGameStartLevelName);
                return;
            }

            WorldData data = _saveLoadService.LoadHandleSave();
            if(data == null)
            {
                return;
            }
            LoadPlayerData(data, hero);
            _stateMachine.Enter<LoadLevelState, string>(data.currentScene);
        }

        private void LoadPlayerData(WorldData data, GameObject hero)
        {
            hero.transform.position = new Vector2(data.x, data.y);
            QuestValues.Instance.QuestList = new List<QuestStage>(data.questValues);
            hero.GetComponent<PlayerHealthController>().CurrentHealth = data.health;
            hero.GetComponent<HealingCapsulesController>().LoadCapsule(data.medkitCount);
            hero.GetComponent<InventoryRoot>().LoadItems(data.items);
        }

        public void Exit()
        {
            
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main.
                GetComponent<CameraFollow>().
                Follow(hero);
        }

    }
}