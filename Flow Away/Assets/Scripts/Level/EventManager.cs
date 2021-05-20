using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event EventHandler OnEventFinished;
    public event EventHandler OnEventStarted;

    public GameObject actionToActivate;
    public string questName;
    public int CurrentEnemyCount { get; set; }

    [SerializeField] private bool isActive;

    [Header("Event Type")]
    public bool enemyCounter;

    [Header("Event Parameters")]
    public int neededEnemyCount;

    private void Start()
    {
        isActive = false;
        actionToActivate.SetActive(isActive);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(QuestValues.Instance.GetStage(questName) == -1)
        {
            QuestValues.Instance.GetStage(questName, true);
        }
        if (collision.gameObject.GetComponent<Player_Movement>() && QuestValues.Instance.GetStage(questName) == 0)
        {
            if (!isActive)
            {
                Activate();
                OnEventStarted?.Invoke(this, EventArgs.Empty);
            }
        }
        if(isActive)
        {
            if (QuestValues.Instance.GetStage(questName) == 1)
            {
                Deactivate();
                OnEventFinished?.Invoke(this, EventArgs.Empty);
            }
            if(enemyCounter)
            {
                if(CurrentEnemyCount == neededEnemyCount)
                {
                    OnEventFinished?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Movement>() && QuestValues.Instance.GetStage(questName, true) == 0)
        {
            if (isActive)
            {
                Deactivate();
                Debug.Log("Active");
            }
        }
    }

    void Activate()
    {
        isActive = true;
        actionToActivate.SetActive(isActive);
    }

    void Deactivate()
    {
        isActive = false;
        actionToActivate.SetActive(isActive);
    }
}
