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

    public void Timer(float _curTime, string gadgetName)
    {
        OnGadgetCooldown?.Invoke(this, new OnGadgetCooldownEventArgs { curTime = _curTime, name = gadgetName });
    }
}

