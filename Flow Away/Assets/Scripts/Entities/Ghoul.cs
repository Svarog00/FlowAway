using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
	[Header("Ghoul")]
	[SerializeField]
	private Transform attackPoint = null;

	public LayerMask playerLayer;

	protected override void Attack()
	{
		//Play Sound
		FindObjectOfType<AudioManager>().Play("ZombieRoar");
		animator.SetTrigger("Attack"); //animate
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, playerLayer); //find the player in circle                                                                                                      //damage him
		foreach (Collider2D enemy in hitEnemies)
		{
			if (enemy.tag == "Player")
			{
				playerHP.Hurt(_damage);
			}
		}
		chill = _chillTime; //Pause between attacks
		StartCoroutine(Cooldown());		
	}

	public override void Hurt(int damage)
	{
		FindObjectOfType<AudioManager>().Play("EnemyHurt");
		_hp -= damage;
		if (_hp <= 0)
		{
			Count();
			FindObjectOfType<AudioManager>().Play("GhoulDead");
			Leave(); //Don't touch the player

			if (_objectPool != null)
				_objectPool.AddToPool(this.gameObject);
			else Destroy(gameObject, 0.5f);
		}
	}
}
