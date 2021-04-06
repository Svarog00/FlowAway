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

    public GadgetActivator gadgetActivator;

    private GadgetManager _gadgetManager;

    private bool _isActve;

    [SerializeField] private float _maxTime = 0f;
    [SerializeField] private float _curTime = 0f;

    public void Awake()
    {
        if (gadgetActivator)
        {
            gadgetActivator.OnGadgetActivated += GadgetActivator_OnGadgetActivated;
        }
        _gadgetManager = FindObjectOfType<GadgetManager>();
    }

    private void GadgetActivator_OnGadgetActivated(object sender, GadgetActivator.OnGadgetActivatedEventArgs e)
    {
        if (e.gadgetName == "PowerShield")
        {
            CanActivate = true;
        }
    }

    private void Start()
    {
        CanActivate = QuestValues.Instance.GetStage("PowerShield") > 0 ? true : false;
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
            _gadgetManager.Timer(_curTime, "PowerShield");
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
