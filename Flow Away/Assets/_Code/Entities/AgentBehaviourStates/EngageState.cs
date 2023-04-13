using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public class EngageState : IBehaviourState
    {
        private EnemyAttack _attack;
        private PlayerHealthController _playerHealth;

        private BehaviourStateMachine _stateMachine;
        private AgentBehaviour _agentContext;

        public EngageState(AgentBehaviour agentContext, BehaviourStateMachine stateMachine)
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
            if (Vector2.Distance(_agentContext.transform.position, _agentContext.Player.transform.position) > _attack.AttackDistance)
            {
                _stateMachine.Enter<ChaseState>();
                return;
            }

            _attack.VectorToPlayer = _agentContext.transform.position - _agentContext.Player.transform.position;
            _attack.Attack();
        }

        public void Exit()
        {
            _playerHealth.RestoreSlots(_agentContext.Weight);
        }
    }
}