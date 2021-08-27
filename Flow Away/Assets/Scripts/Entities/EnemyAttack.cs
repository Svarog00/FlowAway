using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
	[SerializeField] protected Transform _attackPoint = null;
	[SerializeField] protected Animator _animator;
	[SerializeField] protected int _damage;
	[SerializeField] protected float _attackDistance;
	[SerializeField] protected float _chillTime;

	protected float _curChillTime;
	protected float _distanceToPlayer;
	protected Vector2 _vectorToPlayer; 

	public LayerMask playerLayer;

	public float AttackDistance
    {
		get => _attackDistance;
		private set => _attackDistance = value;
    }

	public Vector2 VectorToPlayer
    {
		get => _vectorToPlayer;
		set 
		{ 
			_vectorToPlayer = value;
			_distanceToPlayer = _vectorToPlayer.magnitude;
		}
    }

	public virtual void Attack() { }

	protected IEnumerator Cooldown()
	{
		while (true)
		{
			_curChillTime -= Time.deltaTime;
			if (_curChillTime <= 0)
			{
				yield break;
			}
			yield return null;
		}
	}
}
