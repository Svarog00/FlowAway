using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public class EngageState : IBehaviourState
    {
        private EnemyAttack _attack;
        private PlayerHealthController _playerHealth;

        private AIStateMachine _stateMachine;
        private AgentBehaviour _agentContext;

        public EngageState(AgentBehaviour agentContext, AIStateMachine stateMachine)
        {
            _agentContext = agentContext;
            _stateMachine = stateMachine;
            
            _attack = _agentContext.gameObject.GetComponentInChildren<EnemyAttack>();
        }

        public void Enter()
        {
            _playerHealth = _agentContext.Player.GetComponent<PlayerHealthController>();
            _playerHealth.DecreaseSlots(_agentContext.Weight);
        }

        public void Handle()
        {
            if (_agentContext.DistanceToPlayer > _attack.AttackDistance)
            {
                _stateMachine.Enter<ChaseState>();
            }
            else
            {
                _attack.VectorToPlayer = _agentContext.VectorToPlayer;
                _attack.Attack();
            }
        }

        public void Exit()
        {
            _playerHealth.RestoreSlots(_agentContext.Weight);
        }
    }
}