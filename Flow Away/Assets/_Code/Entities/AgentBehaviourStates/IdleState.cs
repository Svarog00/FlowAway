using CustomEventArguments;

namespace Assets.Scripts.BehaviourStates
{
    public class IdleState : IBehaviourState
    {
        private BehaviourStateMachine _stateMachine;
        private AgentBehaviour _agentContext;

        private EnemyHealth _healthController;

        public IdleState(AgentBehaviour agentContext, BehaviourStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _agentContext = agentContext;

            _healthController = _agentContext.GetComponent<EnemyHealth>();
            _healthController.OnHealthChanged += HealthController_OnHealthChanged;
        }

        public void Enter()
        {
            
        }

        public void Handle()
        {

        }

        public void Exit()
        {
            
        }

        private void HealthController_OnHealthChanged(object sender, OnHealthChangedEventArgs e)
        {
            _stateMachine.Enter<PatrolState>();
        }
    }
}