using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceScript : MonoBehaviour
{
    public event EventHandler OnPlayerDetected;

    [Header("Base script")]
    [SerializeField] private float range = 0f;
    protected Vector3 playerPosition;
    protected bool playerDetected = false;

    // Update is called once per frame
    protected void Update()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, range);
        foreach(Collider2D detectedObject in detectedObjects)
        {
            if(detectedObject.tag == "Player")
            {
                playerPosition = detectedObject.transform.position;
                playerDetected = true;
            }
        }

        if(playerDetected)
        {
            RaycastHit2D[] objects = Physics2D.LinecastAll(transform.position, playerPosition);
            foreach(RaycastHit2D hit in objects)
            {
                if (hit.collider.tag == "Player")
                {
                    Reaction();
                    playerDetected = false;
                    break;
                }
                else if(hit.collider.tag == "Border" || hit.collider.tag == tag)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
    }

    protected virtual void Reaction()
    {
        OnPlayerDetected?.Invoke(this, EventArgs.Empty);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
