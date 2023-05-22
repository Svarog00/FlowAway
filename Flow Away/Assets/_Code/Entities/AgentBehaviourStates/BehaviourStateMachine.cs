using System;
using System.Collections.Generic;

namespace Assets.Scripts.BehaviourStates
{
    public class BehaviourStateMachine
    {
        private IBehaviourState _currentState;
        private Dictionary<Type, IBehaviourState> _states;

        public IBehaviourState CurrentState => _currentState;

        public Dictionary<Type, IBehaviourState> States 
        { 
            get => _states; 
            set => _states = value; 
        }

        public void Work()
        {
            _currentState?.Handle();
        }

        public void Enter<TState>() where TState : class, IBehaviourState
        {
            _currentState = ChangeState<TState>();
            _currentState.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IBehaviourState
        {
            _currentState?.Exit();
            return _states[typeof(TState)] as TState;
        }
    }
}