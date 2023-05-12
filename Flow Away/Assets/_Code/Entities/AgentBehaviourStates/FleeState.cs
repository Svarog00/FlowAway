using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public class FleeState : IBehaviourState
    {
        private BehaviourStateMachine _stateMachine;
        private AgentBehaviour _agentContext;
        private EnemyMovement _enemyMovement;

        private float _fleeDistance;

        public FleeState(AgentBehaviour agentContext, BehaviourStateMachine stateMachine)
        {
            _agentContext = agentContext;
            _stateMachine = stateMachine;

            _fleeDistance = _agentContext.FleeDistance;
            _enemyMovement = _agentContext.gameObject.GetComponent<EnemyMovement>();
        }

        public void Enter()
        {
            
        }

        public void Handle()
        {
            if(Vector2.Distance(_agentContext.transform.position, _agentContext.Player.transform.position) <= _fleeDistance)
            {
                //Найти точку для убегания
            }

            if (Vector2.Distance(_agentContext.transform.position, _agentContext.Player.transform.position) > _fleeDistance)
            {
                _stateMachine.Enter<PatrolState>();
                return;
            }
        }

        public void Exit()
        {
            
        }
    }
}