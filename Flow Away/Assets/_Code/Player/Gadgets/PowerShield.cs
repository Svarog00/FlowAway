using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerShield : Gadget
{
    [Header("Power Shield")]
    [SerializeField] private PowerShieldInstance shieldInstance;
    [SerializeField] private float _maxTime = 0f;

    private bool _isActive;

    private float _curTime = 0f;

    private GameObject _player;

    private void Start()
    {
        _isActive = false;
        _player = gameObject;

        shieldInstance.OnShieldDestroyed += ShieldInstance_OnShieldDestroyed;
    }

    private void ShieldInstance_OnShieldDestroyed(object sender, EventArgs e)
    {
        _isActive = false;
        shieldInstance.gameObject.SetActive(_isActive);
        _curTime = _maxTime;
    }

    private void Update()
    {
        Cooldown();
    }

    public override void HandleActivate()
    {
        if(_curTime <= 0 && IsUnlocked)
        {
            _isActive = true;
            shieldInstance.gameObject.SetActive(_isActive);
            if (_isActive)
            {
                shieldInstance.gameObject.transform.position = _player.transform.position;
            }
        }
    }

    private void Cooldown()
    {
        if (_curTime > 0f)
        {
            _curTime -= Time.deltaTime;
            GadgetManager.Timer(_curTime, _maxTime, _gadgetName);
        }
    }
}
