using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public class PatrolState : IBehaviourState
	{
		private const string PlayerTag = "Player";

		private readonly float _waitTime;

		private readonly Transform[] _patrolSpots;

        private EnemyMovement _movement;

        private int _randomSpot = 0;
        private float _curWaitTime;

        private BehaviourStateMachine _stateMachine;
        private AgentBehaviour _agentContext;

        public PatrolState(AgentBehaviour agentContext, BehaviourStateMachine stateMachine)
        {
			_stateMachine = stateMachine;
			_agentContext = agentContext;

			_waitTime = _agentContext.WaitTime;
			_patrolSpots = _agentContext.PatrolSpots;
			_movement = _agentContext.gameObject.GetComponent<EnemyMovement>();
        }

        public void Enter()
        {
			_agentContext.EnemyDetected = false;
        }

        public void Handle()
        {
			DetectTargets();

			if (_agentContext.EnemyDetected)
			{
				_stateMachine.Enter<ChaseState>();
			}

			if (_patrolSpots.Length <= 0)
			{
				_movement.CanMove = false;
				return;
			}

            _randomSpot = Random.Range(0, _patrolSpots.Length);
            if (Vector2.Distance(_agentContext.transform.position, _patrolSpots[_randomSpot].position) <= 0.6f)
            {
                _movement.CanMove = false;
                if (_curWaitTime <= 0f)
                {
                    _curWaitTime = _waitTime;
                    _randomSpot = Random.Range(0, _patrolSpots.Length);
                    _movement.SetTargetPosition(_patrolSpots[_randomSpot].position);
                    _movement.CanMove = true;
                    return;
                }
               
                _curWaitTime -= Time.deltaTime;
                return;
            }

            _movement.SetTargetPosition(_patrolSpots[_randomSpot].position);
            _movement.CanMove = true;
        }

		private void DetectTargets()
		{
            //find the player in circle
			Collider2D[] detectedEnemies = 
                Physics2D.OverlapCircleAll(_agentContext.transform.position, _agentContext.AgressionDistance, _agentContext.PlayerLayerMask); 
			foreach (Collider2D enemy in detectedEnemies)
			{
                //если произошел агр, то заполняем ссылки на игрока
                if (enemy.tag == PlayerTag && !_agentContext.EnemyDetected) 
				{
					if (_agentContext.Player == null)
					{
						_agentContext.Player = enemy.gameObject;
					}
					_agentContext.EnemyDetected = true;
					break;
				}
			}
		}

        public void Exit()
        {
            
        }
    }
}