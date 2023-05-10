using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public event EventHandler OnEventFinished;
    public event EventHandler OnEventStarted;

    public int CurrentEnemyCount { get; set; }

    [SerializeField] private GameObject _actionToActivate;

    [Header("QuestSpecifics")]
    [SerializeField] private string _questName;
    [SerializeField] private int _neededQuestValue;

    [SerializeField] private bool _isActive;

    [Header("Event Type")]
    [SerializeField] private bool _countEnemyDeaths;

    [Header("Event Parameters")]
    [SerializeField] private int _neededEnemyCount;

    private void Start()
    {
        _isActive = false;
        _actionToActivate?.SetActive(_isActive);
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        HandleActiveEncounter();
    }

    private void HandleActiveEncounter()
    {
        if (_countEnemyDeaths)
        {
            if (CurrentEnemyCount == _neededEnemyCount)
            {
                OnEventFinished?.Invoke(this, EventArgs.Empty);
            }
        }

        if (QuestValues.Instance.GetStage(_questName) == 1)
        {
            Deactivate();
            OnEventFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() && QuestValues.Instance.GetStage(_questName) == _neededQuestValue)
        {
            if (!_isActive)
            {
                Activate();
                OnEventStarted?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && QuestValues.Instance.GetStage(_questName) == 0)
        {
            if (_isActive)
            {
                Deactivate();
            }
        }
    }

    void Activate()
    {
        if (QuestValues.Instance.GetStage(_questName) == -1)
        {
            QuestValues.Instance.Add(_questName);
        }

        _isActive = true;
        _actionToActivate.SetActive(_isActive);
    }

    void Deactivate()
    {
        _isActive = false;
        _actionToActivate.SetActive(_isActive);
    }
}
