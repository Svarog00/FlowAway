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
}
