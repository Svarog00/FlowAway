using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityModuleActivator : MonoBehaviour
{
    private void Start()
    {
        if(QuestValues.Instance.GetStage("Invisibility") == -1)
        {
            QuestValues.Instance.Add("Invisibility");
            Debug.Log("Invisibility module quest given");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && QuestValues.Instance.GetStage("Invisibility") == 0)
        {
            QuestValues.Instance.SetStage("Invisibility", 1);
            Invisibility invisibilityPlayer = collision.GetComponent<Invisibility>();
            invisibilityPlayer.CanActivate = true;
            Debug.Log("Invisibility module activated");
        }
    }
}
