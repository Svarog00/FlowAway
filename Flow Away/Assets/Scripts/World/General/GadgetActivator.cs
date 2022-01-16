using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GadgetActivator : MonoBehaviour
{
    private const string PlayerTag = "Player";
    public string GadgetName;
    private GadgetManager _gadgetManager;

    private void Start()
    {
        _gadgetManager = FindObjectOfType<GadgetManager>();
        if (QuestValues.Instance.GetStage(GadgetName) == -1)
        {
            QuestValues.Instance.Add(GadgetName);
            Debug.Log($"{GadgetName} module quest given");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag && QuestValues.Instance.GetStage(GadgetName) == 0)
        {
            Activate();
        }
    }

    private void Activate()
    {
        QuestValues.Instance.SetStage(GadgetName, 1);
        _gadgetManager.ActivateGadget(GadgetName);
        Debug.Log($"{GadgetName} module quest updated");
    }
}
