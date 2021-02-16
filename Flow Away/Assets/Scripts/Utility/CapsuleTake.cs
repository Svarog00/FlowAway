using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleTake : MonoBehaviour
{
    public CapsulesUI capsulesUI;
    
    private Player_Healing playerHealing;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<Player_Healing>().GetCapsuleCount() < 3)
            {
                playerHealing = other.GetComponent<Player_Healing>();
                Take();
            }
        }
    }

    private void Take()
    {
        FindObjectOfType<AudioManager>().Play("CapsuleTake");

        playerHealing.AddCapsuleCount();
        capsulesUI.AddCapsule(playerHealing.GetCapsuleCount());
        Destroy(gameObject);
    }

}
