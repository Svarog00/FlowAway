using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D player = collider.GetComponent<Rigidbody2D>();
        if(player.gameObject.tag == "Player")
        {
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }


}
