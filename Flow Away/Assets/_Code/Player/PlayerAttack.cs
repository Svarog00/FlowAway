﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public Animator animator;
	public Transform attackPoint;
	public LayerMask enemyLayers;
	
	[SerializeField] private float _attackRange;
	[SerializeField] private float _attackDelay = 0.5f;
	[SerializeField] private int _damage = 15;
	private float _delay;

	// Update is called once per frame
	void Update()
	{
		Cooldown();
	}

	public void Melee()
	{
		if(_delay <= 0f)
        {
			//animate melee attack
			animator.SetTrigger("Melee_Strike");
        }
	}

	private void DealMeleeDamage()
	{
        FindObjectOfType<AudioManager>().Play("SwordSwing");
        //detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayers);
		//damage them
		IDamagable tmpDamagable;
		foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.gameObject.tag.Contains("Player") && enemy.TryGetComponent(out tmpDamagable) && !enemy.CompareTag("Shield"))
            {
                tmpDamagable.Hurt(_damage);
            }
        }
        _delay = _attackDelay;
    }

	private void Cooldown()
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
		Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
	}
}
