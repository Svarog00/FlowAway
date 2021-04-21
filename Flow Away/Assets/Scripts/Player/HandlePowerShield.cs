using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandlePowerShield : Gadget
{
    public event EventHandler<OnShieldActivatedEventArgs> OnShieldActivated;

    public class OnShieldActivatedEventArgs
    {
        public bool isAcive;
    }
    [Header("Power shield")]
    private bool _isActve;

    [SerializeField] private float _maxTime = 0f;
    [SerializeField] private float _curTime = 0f;

    private void Start()
    {
        CanActivate = QuestValues.Instance.GetStage(gadgetName) > 0 ? true : false;
        PowerShield powerShield = GetComponentInChildren<PowerShield>();
        powerShield.OnShieldDestroyed += PowerShield_OnShieldDestroyed;
    }

    private void PowerShield_OnShieldDestroyed(object sender, EventArgs e)
    {
        _isActve = false;
        _curTime = _maxTime;
    }

    void Update()
    {
        if (_curTime > 0f)
        {
            _curTime -= Time.deltaTime;
            GadgetManager.Timer(_curTime, _maxTime ,gadgetName);
        }

        if (Input.GetButtonDown("Second Module") && CanActivate)
        {
            if (!_isActve && _curTime <= 0f)
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
