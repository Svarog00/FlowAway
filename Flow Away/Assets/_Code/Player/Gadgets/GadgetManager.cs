using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Rendering.DebugUI;
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
        public string Name;
        public bool IsActive;
    }

    public List<Gadget> Gadgets => _gadgets;

    [SerializeField] private List<Gadget> _gadgets;

    public void CooldownTimer(float curTime, float maxTime, string gadgetName)
    {
        OnGadgetCooldown?.Invoke(this, new OnGadgetCooldownEventArgs { curTime = 1 - curTime/maxTime, name = gadgetName });
    }

    public void ActivateGadget(string gadgetName)
    {
        if (QuestValues.Instance.GetStage(gadgetName) > 0)
        {
            return;
        }

        foreach(var gadget in _gadgets)
        {
            if(gadget.Name.Equals(gadgetName))
            {
                gadget.ToggleUnlock(true);
                QuestValues.Instance.Add(gadgetName);
                QuestValues.Instance.SetStage(gadgetName, 1);
                OnGadgetActivate?.Invoke(this, new OnGadgetActivateEventArgs { Name = gadgetName, IsActive = true });
                return;
            }
        }
    }

    public void SetGadgetStatus(List<bool> abilitiesStatus)
    {
        for(int i = 0; i < _gadgets.Count; i++) 
        {
            _gadgets[i].ToggleUnlock(abilitiesStatus[i]);
            QuestValues.Instance.SetStage(_gadgets[i].name, 0);
            OnGadgetActivate?.Invoke(this, new OnGadgetActivateEventArgs { Name = _gadgets[i].Name, IsActive = _gadgets[i].IsUnlocked });
        } 
    }

    public List<bool> GetAbilitiesStatus()
    {
        var statuses = new List<bool>();
        foreach (var value in _gadgets)
        {
            statuses.Add(value.IsUnlocked);
        }

        return statuses;
    }
}

