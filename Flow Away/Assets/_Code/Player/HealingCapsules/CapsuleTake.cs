using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleTake : MonoBehaviour
{  
    private HealingCapsulesController _healingCapsulesController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<HealingCapsulesController>().CapsulesCount < 3)
            {
                _healingCapsulesController = other.GetComponent<HealingCapsulesController>();
                Take();
            }
        }
    }

    private void Take()
    {
        AudioManager.Instance.Play("CapsuleTake");

        _healingCapsulesController.AddCapsule();
        Destroy(gameObject);
    }

}
