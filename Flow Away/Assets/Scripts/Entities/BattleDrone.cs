using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDrone : Enemy
{
    [Header("Battle drone")]
    public float meleeRange;
    public GameObject shotPrefab;
    public Transform firePoint;

    protected override void Attack()
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

    public override void Hurt(int damage)
    {
        FindObjectOfType<AudioManager>().Play("EnemyHurt");
        hp -= damage;
        if (hp <= 0)
        {
            Count();
            FindObjectOfType<AudioManager>().Play("Meow");
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
