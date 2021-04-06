using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }


}
