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

            //Если игрок вне дистанции агрессии, то продолжать какое-то время преследовать
            if (Vector2.Distance(_agentContext.transform.position, _agentContext.Player.transform.position) > _agentContext.AgressionDistance) 
			{
				_elapsedTime += Time.deltaTime;

                //Если цель уже слишком далеко, то возврат в патрулю
                if (_elapsedTime >= _agentContext.ChaseTimeOut) 
				{
					_stateMachine.Enter<PatrolState>();
					_elapsedTime = 0;
				}
			}
			else if (Vector2.Distance(_agentContext.transform.position, _agentContext.Player.transform.position) <= _enemyAttack.AttackDistance) //Если игрок слишком близко, то остановиться для атаки
			{
				_elapsedTime = 0f;
                //Если игрока атакует не слишком много противников, то можно атаковать
                if (_playerHP.FreeSlots > _agentContext.Weight) 
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