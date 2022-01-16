using Assets.Scripts.BehaviourStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Patroling, Chasing, Attacking };

public class EnemyBehavior : AgentBehaviour
{
	private void Start()
	{
		Init();

		StateMachine.States = new Dictionary<Type, IBehaviourState>
		{
			[typeof(PatrolState)] = new PatrolState(this, StateMachine),
			[typeof(ChaseState)] = new ChaseState(this, StateMachine),
			[typeof(EngageState)] = new EngageState(this, StateMachine),
		};

		StateMachine.Enter<PatrolState>();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, AgressionDistance);
	}

	/*
	private void DetectTargets()
	{
		Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, AgressionDistance, layerMask); //find the player in circle
		foreach (Collider2D enemy in detectedEnemies)
		{
			if (enemy.tag == "Player" && !_playerDetected) //если произошел агр, то заполняем ссылки на игрока
			{
				if (player == null)
				{
					player = enemy.gameObject;
					Invisibility playerInsibility = player.GetComponent<Invisibility>();
					playerInsibility.OnInsibilityEnable += PlayerInsibility_OnInsibilityEnable;
					playerHP = player.GetComponent<PlayerHealthController>();
				}
				_playerDetected = true;
				break;
			}
		}
	}*/

	/*IEnumerator PatrolState()
	{
		_currentState = EnemyStates.Patroling;

		while (_currentState == EnemyStates.Patroling)
		{
			DetectTargets();

			if(_playerDetected)
			{
				StartCoroutine(ChaseState());
				yield break;
			}

			if (patrolSpots.Length > 0)
			{
				_randomSpot = Random.Range(0, patrolSpots.Length);
				if (Vector2.Distance(transform.position, patrolSpots[_randomSpot].position) <= 0.6f)
				{
					_enemyMovement.CanMove = false;
					if (_curWaitTime <= 0f)
					{
						_curWaitTime = _waitTime;
						_randomSpot = Random.Range(0, patrolSpots.Length);
						_enemyMovement.SetTargetPosition(patrolSpots[_randomSpot].position);
						_enemyMovement.CanMove = true;
					}
					else
					{
						_curWaitTime -= Time.deltaTime;
					}
				}
				else
				{
					_enemyMovement.SetTargetPosition(patrolSpots[_randomSpot].position);
					_enemyMovement.CanMove = true;
				}
			}
			else
			{
				_enemyMovement.CanMove = false;
			}

			yield return null;
		}
	}

	IEnumerator ChaseState()
	{
		_currentState = EnemyStates.Chasing;
		Vector3 oldTargetPosition = player.transform.position;
		_enemyMovement.SetTargetPosition(oldTargetPosition);
		float elapsedTime = 0f;
		while (_currentState == EnemyStates.Chasing)
		{
			if(oldTargetPosition != player.transform.position)
            {
				oldTargetPosition = player.transform.position;
				_enemyMovement.SetTargetPosition(oldTargetPosition);
			}

			_enemyMovement.CanMove = true;

			if (distanceToPlayer > _agressionDistance)
			{
				elapsedTime += Time.deltaTime;

				if(elapsedTime >= _chaseTimeOut)
                {
					_currentState = EnemyStates.Patroling;
					_playerDetected = false;
					StartCoroutine(PatrolState());
					yield break;
				}
			}
			else if (distanceToPlayer <= _enemyAttack.AttackDistance) //Если игрок слишком близко, то остановиться для атаки
			{
				elapsedTime = 0f;
				_enemyMovement.CanMove = false;
				if (!canAttack && playerHP.GetFreeSlots() > _weight) //Если игрока атакует не слишком много противников, то можно атаковать
				{
					StartCoroutine(EngageState());
					yield break;
				}
			}
			else
            {
				elapsedTime = 0f;
			}

			yield return null;
		}
	}

	IEnumerator EngageState()
	{
		_currentState = EnemyStates.Attacking;
		canAttack = true;
		playerHP.DecreaseSlots(_weight);
		while (_currentState == EnemyStates.Attacking)
		{
			if (distanceToPlayer > _enemyAttack.AttackDistance)
			{
				canAttack = false;
				playerHP.RestoreSlots(_weight);
				StartCoroutine(ChaseState());
				yield break;
			}
			else
			{
				_enemyAttack.VectorToPlayer = vectorToPlayer;
				_enemyAttack.Attack();
			}

			yield return null;
		}
	}*/
}
