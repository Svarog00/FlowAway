using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : Boss
{
    [Header("Gargoyle")]

   // public Observer observer;

    public Transform attackPoint;

    public float meleeRange;

    public LayerMask playerLayer;

   // public GameObject shotPrefab;
   // public Transform firePoint;


    private string currentState;
    

    private void OnEnable()
    {
        base.healthPoints = 300;
        currentState = "Idle";
    }

    private void Attack()
    {
        
    }

    private void Flying()
    {

    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        //animator.Play(newState);

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
