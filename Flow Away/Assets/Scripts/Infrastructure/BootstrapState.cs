using Assets.Scripts.Infrustructure;
using Assets.Scripts.Services;
using System;

namespace Assets.Scripts.Infrastructure
{
    class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
             RegisterServices();

            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("ExperimentalScene");
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputSerice();
        }

        public void Exit()
        {
            
        }

        private static IInputService RegisterInputSerice()
        {
            return new InputService();
        }
    }
}
