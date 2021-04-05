using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDrone : Enemy
{
    [Header("Battle drone")]
    public float meleeRange;

    public LayerMask playerLayer;

    public GameObject shotPrefab;
    public Transform firePoint;

    [SerializeField] private float _pushForce = 0;

    protected override void Attack()
    {
        if(_distanceToPlayer > meleeRange)
        {
            //создание новго выстрела
            GameObject shotTransform = Instantiate(shotPrefab, firePoint.position, firePoint.rotation.normalized);
            //перемещение
            shotTransform.GetComponent<ShotScript>().speed = new Vector2(5, 5) * -direction;
            shotTransform.GetComponent<ShotScript>().shooter = gameObject;
            FindObjectOfType<AudioManager>().Play("Shot");

            chill = _chillTime; //Pause between attacks
            StartCoroutine(Cooldown());
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Roar");
            animator.SetTrigger("Attack"); //animate
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, meleeRange, playerLayer); //find the player in circle
                                                                                                                 //damage them
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Player"))
                {
                    playerHP.Hurt(_damage);
                    playerPosition.AddForce(direction*_pushForce);
                }
            }
            chill = _chillTime; //Pause between attacks
            StartCoroutine(Cooldown());
        }
    }

    public override void Hurt(int damage)
    {
        FindObjectOfType<AudioManager>().Play("HurtRobotSound");
        _hp -= damage;
        if (_hp <= 0)
        {
            Count();
            FindObjectOfType<AudioManager>().Play("RobotDeathSound");
            Leave(); //Don't touch the player
            Destroy(gameObject, 0.5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (firePoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(firePoint.position, meleeRange);
    }
}
