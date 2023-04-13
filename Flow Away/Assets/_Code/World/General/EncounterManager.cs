using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public event EventHandler OnEventFinished;
    public event EventHandler OnEventStarted;

    public GameObject ActionToActivate;
    public int CurrentEnemyCount { get; set; }
    [SerializeField] private string QuestName;

    [SerializeField] private bool isActive;

    [Header("Event Type")]
    [SerializeField] private bool _enemyCounter;

    [Header("Event Parameters")]
    [SerializeField] private int _neededEnemyCount;

    private void Start()
    {
        isActive = false;
        ActionToActivate.SetActive(isActive);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(QuestValues.Instance.GetStage(QuestName) == -1)
        {
            QuestValues.Instance.GetStage(QuestName, true);
        }

        if (collision.gameObject.GetComponent<PlayerMovement>() && QuestValues.Instance.GetStage(QuestName) == 0)
        {
            if (!isActive)
            {
                Activate();
                OnEventStarted?.Invoke(this, EventArgs.Empty);
            }
        }

        if(!isActive)
        {
            return;
        }

        if (QuestValues.Instance.GetStage(QuestName) == 1)
        {
            Deactivate();
            OnEventFinished?.Invoke(this, EventArgs.Empty);
        }

        if (_enemyCounter)
        {
            if (CurrentEnemyCount == _neededEnemyCount)
            {
                OnEventFinished?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && QuestValues.Instance.GetStage(QuestName, true) == 0)
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
        ActionToActivate.SetActive(isActive);
    }

    void Deactivate()
    {
        isActive = false;
        ActionToActivate.SetActive(isActive);
    }
}
