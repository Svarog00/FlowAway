using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Class for notifying observers about state of gadgets
public class GadgetManager : MonoBehaviour
{
    //Cooldown event for gadget icons
    public event EventHandler<OnGadgetCooldownEventArgs> OnGadgetCooldown;
    public class OnGadgetCooldownEventArgs : EventArgs
    {
        public string name;
        public float curTime;
    }

    //Gadget activation
    public event EventHandler<OnGadgetActivateEventArgs> OnGadgetActivate;
    public class OnGadgetActivateEventArgs : EventArgs
    {
        public string name;
    }

    public void Timer(float curTime, float maxTime, string gadgetName)
    {
        OnGadgetCooldown?.Invoke(this, new OnGadgetCooldownEventArgs { curTime = 1 - curTime/maxTime, name = gadgetName });
    }

    public void ActivateGadget(string gadgetName)
    {
        QuestValues.Instance.Add(gadgetName);
        QuestValues.Instance.SetStage(gadgetName, 1);
        OnGadgetActivate?.Invoke(this, new OnGadgetActivateEventArgs { name = gadgetName });
    }
}

