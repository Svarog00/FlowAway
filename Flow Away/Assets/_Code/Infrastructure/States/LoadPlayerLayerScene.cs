using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrustructure;
using InventorySystem;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class LoadPlayerSceneState : IState
    {
        private const string PlayerSceneName = "PlayerLayer";
        private const string StartSceneName = "ExperimentalScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadPlayerSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
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

            _stateMachine.Enter<LoadLevelState, string>(StartSceneName);
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