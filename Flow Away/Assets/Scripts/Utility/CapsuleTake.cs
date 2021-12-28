using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleTake : MonoBehaviour
{  
    private PlayerHealthController _playerHealthController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<PlayerHealthController>().GetCapsuleCount() < 3)
            {
                _playerHealthController = other.GetComponent<PlayerHealthController>();
                Take();
            }
        }
    }

    private void Take()
    {
        AudioManager.Instance.Play("CapsuleTake");

        _playerHealthController.AddCapsule();
        Destroy(gameObject);
    }

}
