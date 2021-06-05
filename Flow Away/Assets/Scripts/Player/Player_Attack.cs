using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
	public Animator animator;
	public Transform attackPoint;
	public LayerMask enemyLayers;
	public Rigidbody2D rb2;
	public WeaponScript weapon;

	public float attackRange;
	public float attackDelay = 0.5f;

	[SerializeField]
	private int Damage = 15;
	private float _delay;

	// Update is called once per frame
	void Update()
	{
		GetInput();
		Cooldown();
	}

	void GetInput()
	{
		if (Input.GetButtonDown("MeleeStrike") && _delay <= 0f)
		{
			Melee();
		}

		if (Input.GetButtonDown("GunFire"))
		{
			weapon.GetComponent<WeaponScript>().Attack();
		}
	}

	void Melee()
	{
		FindObjectOfType<AudioManager>().Play("SwordSwing");
		//animate melee attack
		animator.SetTrigger("Melee_Strike");
		//detect enemies in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
		//damage them
		foreach(Collider2D enemy in hitEnemies)
		{
			if(enemy.gameObject != gameObject && enemy.GetComponent<IDamagable>() != null)
			{
				enemy.GetComponent<IDamagable>().Hurt(Damage);
				// enemy.GetComponent<Health>.Hurt();
			}
		}
		_delay = attackDelay;
	}

	void Cooldown()
    {
		if (_delay > 0f)
		{
			_delay -= Time.deltaTime;
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}
