using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
	[Header("Ghoul")]
	[SerializeField]
	private Transform attackPoint = null;

	public LayerMask playerLayer;

	private void OnEnable()
	{
		hp = 30;
	}

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
				playerHP.Hurt(damage);
			}
		}
		chill = _chillTime; //Pause between attacks
		StartCoroutine(Cooldown());		
	}

	public override void Hurt(int damage)
	{
		FindObjectOfType<AudioManager>().Play("EnemyHurt");
		hp -= damage;
		if (hp <= 0)
		{
			FindObjectOfType<AudioManager>().Play("GhoulDead");
			Leave(); //Don't touch the player
			Destroy(gameObject, 0.5f);
		}
	}
}
