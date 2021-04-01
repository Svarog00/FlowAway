using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandlePowerShield : MonoBehaviour
{
    public event EventHandler<OnShieldActivatedEventArgs> OnShieldActivated;
    public class OnShieldActivatedEventArgs
    {
        public bool isAcive;
    }


    public bool CanActivate { get; set; }

    private bool _isActve;

    private void Start()
    {
        CanActivate = QuestValues.Instance.GetStage("PowerShield") > 0 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Second Module") && CanActivate)
        {
            if (!_isActve)
            {
                _isActve = true;
                OnShieldActivated?.Invoke(this, new OnShieldActivatedEventArgs { isAcive = _isActve });
            }
            else
            {
                _isActve = false;
                OnShieldActivated?.Invoke(this, new OnShieldActivatedEventArgs { isAcive = _isActve });
            }
        }
    }
}
