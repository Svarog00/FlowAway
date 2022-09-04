using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrustructure;

namespace Assets.Scripts.Infrastructure
{
    public class LoadPlayerSceneState : IState
    {
        private const string PlayerSceneName = "PlayerLayer";

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

        }

        public void Exit()
        {
            
        }
    }
}