using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionAttack : EnemyAttack
{
	[SerializeField] private GameObject _shotPrefab;
	[SerializeField] private Transform _firePoint;
	[SerializeField] private float _meleeRange;

	public override void Attack()
	{
		if(curChillTime > 0)
        {
            return;
		}

        if (distanceToPlayer <= _meleeRange)
        {
            AudioManager.Instance.Play("Roar");
            Animator.SetTrigger("Attack"); //animate

        }
        else if (distanceToPlayer > _meleeRange)
        {
            GameObject shotTransform = Instantiate(_shotPrefab, _firePoint.position, _firePoint.rotation.normalized);
            shotTransform.GetComponent<ShotScript>().speed = new Vector2(5, 5) * -(vectorToPlayer / distanceToPlayer);
            shotTransform.GetComponent<ShotScript>().shooter = gameObject;
            AudioManager.Instance.Play("Shot");
        }

        curChillTime = ChillTime; //Pause between attacks
        StartCoroutine(Cooldown());
    }

    public override void DealMeleeDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, _meleeRange, playerLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag.Contains("Player"))
            {
                enemy.GetComponent<IDamagable>().Hurt(Damage);
            }
        }
    }
}
