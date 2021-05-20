using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;
    [SerializeField] string questName;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && QuestValues.Instance.GetStage(questName) != 2)
        {
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }


}
