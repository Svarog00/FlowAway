using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GadgetManager : MonoBehaviour
{
    public event EventHandler<OnGadgetCooldownEventArgs> OnGadgetCooldown;
    public class OnGadgetCooldownEventArgs : EventArgs
    {
        public string name;
        public float curTime;
    }

    public event EventHandler<OnGadgetActivateEventArgs> OnGadgetActivate;

    public class OnGadgetActivateEventArgs : EventArgs
    {
        public string name;
    }

    public void Timer(float _curTime, float _maxTime , string gadgetName)
    {

        OnGadgetCooldown?.Invoke(this, new OnGadgetCooldownEventArgs { curTime = 1 - _curTime/_maxTime, name = gadgetName });
    }

    public void ActivateGadget(string gadgetName)
    {
        OnGadgetActivate?.Invoke(this, new OnGadgetActivateEventArgs { name = gadgetName });
    }
}

