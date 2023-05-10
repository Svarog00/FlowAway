using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected LayerMask playerLayer;

	[Header("External objects")]
	[SerializeField] protected Transform _attackPoint = null;
	[SerializeField] protected Animator _animator;

	[Header("Attack characteristics")]
	[SerializeField] protected int Damage;
	[SerializeField] protected float attackDistance;
	[SerializeField] protected float chillTime;

	protected float curChillTime;
	protected float distanceToPlayer;
	protected Vector2 vectorToPlayer;

	public float AttackDistance => attackDistance;

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

	public virtual void DealMeleeDamage() { }

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

	private void OnDrawGizmosSelected()
	{
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
