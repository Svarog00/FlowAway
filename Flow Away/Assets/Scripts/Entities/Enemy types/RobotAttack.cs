using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : EnemyAttack
{
	[SerializeField] private GameObject _shotPrefab;
	[SerializeField] private Transform _firePoint;
	[SerializeField] private float _meleeRange;
	[SerializeField] private float _pushForce;

	public override void Attack()
	{
		if (curChillTime <= 0)
		{
			if (distanceToPlayer > _meleeRange)
			{
				GameObject shotTransform = Instantiate(_shotPrefab, _firePoint.position, _firePoint.rotation.normalized);
				shotTransform.GetComponent<ShotScript>().speed = new Vector2(5, 5) * -(vectorToPlayer / distanceToPlayer);
				shotTransform.GetComponent<ShotScript>().shooter = gameObject;
				FindObjectOfType<AudioManager>().Play("Shot");
			}
			else
			{
				Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, _meleeRange, playerLayer); //find the player in circle
																															  //damage them
				foreach (Collider2D enemy in hitEnemies)
				{
					if (enemy.tag.Contains("Player"))
					{
						enemy.GetComponent<IDamagable>().Hurt(_damage);
						enemy.GetComponent<Rigidbody2D>().AddForce(-(vectorToPlayer/distanceToPlayer) * _pushForce);
					}
				}
			}

			curChillTime = chillTime; //Pause between attacks
			StartCoroutine(Cooldown());
		}
	}
}
