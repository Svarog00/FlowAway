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
			_movement.CanMove = true;

			_playerHP = _agentContext.Player.GetComponent<PlayerHealthController>();
		}

		public void Handle()
		{
			if (_oldTargetPosition != _agentContext.Player.transform.position)
			{
				_oldTargetPosition = _agentContext.Player.transform.position;
				_movement.SetTargetPosition(_oldTargetPosition);
			}

			if (_agentContext.DistanceToPlayer > _agentContext.AgressionDistance) //Если игрок вне дистанции агрессии, то продолжать какое-то время преследовать
			{
				_elapsedTime += Time.deltaTime;

				if (_elapsedTime >= _agentContext.ChaseTimeOut) //Если цель уже слишком далеко, то возврат в патрулю
				{
					_stateMachine.Enter<PatrolState>();
				}
			}
			else if (_agentContext.DistanceToPlayer <= _enemyAttack.AttackDistance) //Если игрок слишком близко, то остановиться для атаки
			{
				_elapsedTime = 0f;
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
            _movement.CanMove = false;
        }
    }
}