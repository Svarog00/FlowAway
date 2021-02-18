using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerLion : Enemy
{
	[Header("Flower Lion")]
	public Transform attackPoint;

	public float meleeRange;

	public LayerMask playerLayer;

	public GameObject shotPrefab;
	public Transform firePoint;

	private void OnEnable()
	{
		hp = 60;
	}

	protected override void Attack()
	{
		if(_distanceToPlayer <= meleeRange)
		{
			FindObjectOfType<AudioManager>().Play("Roar");
			animator.SetTrigger("Attack"); //animate
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeRange, playerLayer); //find the player in circle
			//damage them
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
		else if(_distanceToPlayer > meleeRange)
		{
			//создание новго выстрела
			GameObject shotTransform = Instantiate(shotPrefab, firePoint.position, firePoint.rotation.normalized);
			//перемещение
			shotTransform.GetComponent<ShotScript>().speed = new Vector2(5, 5) * -direction;
			shotTransform.GetComponent<ShotScript>().shooter = gameObject;
			FindObjectOfType<AudioManager>().Play("Shot");

			chill = _chillTime; //Pause between attacks
			StartCoroutine(Cooldown()) ;
		}
	}

	public override void Hurt(int damage)
	{
		FindObjectOfType<AudioManager>().Play("EnemyHurt");
		hp -= damage;
		if (hp <= 0)
		{
			FindObjectOfType<AudioManager>().Play("Meow");
			Leave(); //Don't touch the player
			Destroy(gameObject, 0.5f);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(attackPoint.position, meleeRange);
	}
}
