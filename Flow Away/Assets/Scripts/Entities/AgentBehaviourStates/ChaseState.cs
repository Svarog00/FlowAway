using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public class ChaseState : IBehaviourState
	{
        private PlayerHealthController _playerHP;

        private EnemyMovement _movement;
		private EnemyAttack _enemyAttack;

        private BehaviourStateMachine _stateMachine;
		private AgentBehaviour _agentContext;

        private Vector3 _oldTargetPosition;
        
		private float _elapsedTime = 0f;

        public ChaseState(AgentBehaviour agentContext, BehaviourStateMachine stateMachine)
        {
			_agentContext = agentContext;
			_stateMachine = stateMachine;

			_movement = _agentContext.gameObject.GetComponent<EnemyMovement>();
			_enemyAttack = _agentContext.gameObject.GetComponentInChildren<EnemyAttack>();
        }

		public void Enter()
		{
			_oldTargetPosition = _agentContext.Player.transform.position;
			_movement.SetTargetPosition(_oldTargetPosition);

			_playerHP = _agentContext.Player.GetComponent<PlayerHealthController>();
		}

		public void Handle()
		{
			if (_oldTargetPosition != _agentContext.Player.transform.position)
			{
				_oldTargetPosition = _agentContext.Player.transform.position;
				_movement.SetTargetPosition(_oldTargetPosition);
			}

			_movement.CanMove = true;

			if (_agentContext.DistanceToPlayer > _agentContext.AgressionDistance)
			{
				_elapsedTime += Time.deltaTime;

				if (_elapsedTime >= _agentContext.ChaseTimeOut)
				{
					_stateMachine.Enter<PatrolState>();
				}
			}
			else if (_agentContext.DistanceToPlayer <= _enemyAttack.AttackDistance) //Если игрок слишком близко, то остановиться для атаки
			{
				_elapsedTime = 0f;
				_movement.CanMove = false;
				if (_playerHP.GetFreeSlots() > _agentContext.Weight) //Если игрока атакует не слишком много противников, то можно атаковать
				{
					_stateMachine.Enter<EngageState>();
				}
			}
			else
			{
				_elapsedTime = 0f;
			}
		}

        public void Exit()
        {
            
        }
    }
}