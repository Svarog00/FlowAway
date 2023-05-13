using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public class PatrolState : IBehaviourState
	{
		private const string PlayerTag = "Player";
        private const float MovementAccuracy = 0.5f;

        private readonly float _waitTime;

		private readonly Transform[] _patrolSpots;

        private EnemyMovement _movement;

        private int _randomSpot = -1;
        private float _curWaitTime;
        private bool _isEnemyDetected;

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
            _isEnemyDetected = false;

            SetRandomPatrolSpot();
        }

        public void Handle()
        {
			DetectTargets();

			if (_isEnemyDetected)
			{
				_stateMachine.Enter<ChaseState>();
			}

            if (_randomSpot == -1)
            {
                _movement.StartMove(false);
                return;
            }

            if (Vector2.Distance(_agentContext.transform.position, _patrolSpots[_randomSpot].position) <= MovementAccuracy)
            {
                _movement.StartMove(false);
                if (_curWaitTime <= 0f)
                {
                    _curWaitTime = _waitTime;
                    SetRandomPatrolSpot();
                    return;
                }

                _curWaitTime -= Time.deltaTime;
                return;
            }
        }

        private void SetRandomPatrolSpot()
        {
            if(_patrolSpots.Length <= 0)
            {
                _randomSpot = -1;
                return;
            }
            _randomSpot = Random.Range(0, _patrolSpots.Length);
            _movement.SetTargetPosition(_patrolSpots[_randomSpot].position);
        }

        private void DetectTargets()
		{
            //find the player in circle
			Collider2D[] detectedEnemies = 
                Physics2D.OverlapCircleAll(_agentContext.transform.position, _agentContext.AgressionDistance, _agentContext.PlayerLayerMask); 
			foreach (Collider2D enemy in detectedEnemies)
			{
                //если произошел агр, то заполняем ссылки на игрока
                if (enemy.tag == PlayerTag && !_isEnemyDetected) 
				{
					if (_agentContext.Player == null)
					{
						_agentContext.Player = enemy.gameObject;
					}
                    _isEnemyDetected = true;
					break;
				}
			}
		}

        public void Exit()
        {
            
        }
    }
}