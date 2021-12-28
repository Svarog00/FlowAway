using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services;

namespace Assets.Scripts.Infrustructure
{
    public class Game
    {
        public static IInputService InputService;

        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}