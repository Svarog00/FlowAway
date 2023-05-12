using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrustructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded, LoadSceneMode.Additive);
        }

        private void OnLoaded()
        {
            /*GameObject hero = Object.FindObjectOfType<PlayerControl>().gameObject;
            hero.transform.position = GameObject.FindGameObjectWithTag(InitialPointTag).transform.position;*/

            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }
    }
}