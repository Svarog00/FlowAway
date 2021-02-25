using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    public event EventHandler OnEventFinished;

    public GameObject actionToActivate;
    public string questName;


    private void Start()
    {
        actionToActivate.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && QuestValues.Instance.GetStage(questName, true) == 0)
        {
            QuestValues.Instance.GetStage(questName, true);
            if (!actionToActivate.activeSelf)
            {
                actionToActivate.SetActive(true);
                Debug.Log("Active");
            }
            if (QuestValues.Instance.GetStage(questName) == 1)
            {
                OnEventFinished?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && QuestValues.Instance.GetStage(questName, true) == 0)
        {
            if (actionToActivate.activeSelf)
            {
                actionToActivate.SetActive(false);
                Debug.Log("Active");
            }
        }
    }
}
