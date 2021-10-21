using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
	[SerializeField] protected Transform _attackPoint = null;
	[SerializeField] protected Animator _animator;
	[SerializeField] protected int _damage;
	[SerializeField] protected float attackDistance;
	[SerializeField] protected float chillTime;

	protected float curChillTime;
	protected float distanceToPlayer;
	protected Vector2 vectorToPlayer; 

	public LayerMask playerLayer;

	public float AttackDistance
    {
		get => attackDistance;
		private set => attackDistance = value;
    }

	public Vector2 VectorToPlayer
    {
		get => vectorToPlayer;
		set 
		{ 
			vectorToPlayer = value;
			distanceToPlayer = vectorToPlayer.magnitude;
		}
    }

	public virtual void Attack() { }

	protected IEnumerator Cooldown()
	{
		while (true)
		{
			curChillTime -= Time.deltaTime;
			if (curChillTime <= 0)
			{
				yield break;
			}
			yield return null;
		}
	}
}
