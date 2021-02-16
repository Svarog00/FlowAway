using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : Enemy
{
    [Header("Gargoyle")]

    public Transform attackPoint;

    public float meleeRange;

    public LayerMask playerLayer;

    public GameObject shotPrefab;
    public Transform firePoint;

    private string currentState;

    private void OnEnable()
    {
        base.hp = 300;
        currentState = "Idle";
    }

    protected override void Attack()
    {
        
    }

    public override void Hurt(int damage)
    {

    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
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
