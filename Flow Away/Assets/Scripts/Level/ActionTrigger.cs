using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
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
            if(!actionToActivate.activeSelf)
            {
                actionToActivate.SetActive(true);
                Debug.Log("Active");
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
