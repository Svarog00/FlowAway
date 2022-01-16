using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.Infrustructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), ServiceLocator.Container);
        }
    }
}