using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GadgetActivator : MonoBehaviour
{
    public string GadgetName;

    public event EventHandler<OnGadgetActivatedEventArgs> OnGadgetActivated;

    public class OnGadgetActivatedEventArgs
    {
        public string gadgetName;
    }

    private void Start()
    {
        if (QuestValues.Instance.GetStage(GadgetName) == -1)
        {
            QuestValues.Instance.Add(GadgetName);
            Debug.Log($"{GadgetName} module quest given");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && QuestValues.Instance.GetStage(GadgetName) == 0)
        {
            QuestValues.Instance.SetStage(GadgetName, 1);
            OnGadgetActivated?.Invoke(this, new OnGadgetActivatedEventArgs { gadgetName = GadgetName });
            Debug.Log($"{GadgetName} module quest given");
        }
    }
}
