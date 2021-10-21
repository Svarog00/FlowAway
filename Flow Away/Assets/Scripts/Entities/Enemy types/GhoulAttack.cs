using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAttack : EnemyAttack
{
	public override void Attack()
	{
		if(curChillTime <= 0)
        {
			//Play Sound
			FindObjectOfType<AudioManager>().Play("ZombieRoar");
			_animator.SetTrigger("Attack"); //animate
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, attackDistance, playerLayer); //find the player in circle                                                                                                      //damage him
			foreach (Collider2D enemy in hitEnemies)
			{
				if (enemy.tag.Contains("Player"))
				{
					enemy.GetComponent<IDamagable>().Hurt(_damage);
				}
			}
			curChillTime = chillTime; //Pause between attacks
			StartCoroutine(Cooldown());
        }
	}
}
