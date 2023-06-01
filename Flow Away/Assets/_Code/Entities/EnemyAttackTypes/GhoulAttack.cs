using UnityEngine;

public class GhoulAttack : EnemyAttack
{
	public override void Attack()
	{
		if(curChillTime <= 0)
        {
			//Play Sound
			AudioManager.Instance.Play("ZombieRoar");
			Animator.SetTrigger("Attack"); //animate

            curChillTime = ChillTime; //Pause between attacks
            StartCoroutine(Cooldown());
        }
        
    }

	public override void DealMeleeDamage()
	{
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackDistance, playerLayer); //find the player in circle                                                                                                      //damage him
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag.Contains("Player"))
            {
                enemy.GetComponent<IDamagable>().Hurt(Damage);
            }
        }
    }
}
