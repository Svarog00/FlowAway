using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerShield : Gadget
{
    [Header("Power Shield")]
    [SerializeField] private PowerShieldInstance shieldInstance;

    private bool _isActve;

    [SerializeField] private float _maxTime = 0f;
    [SerializeField] private float _curTime = 0f;

    private GameObject _player;

    private void Start()
    {
        _isActve = false;
        _player = gameObject;

        shieldInstance.OnShieldDestroyed += ShieldInstance_OnShieldDestroyed;

        CheckAviability();
    }

    private void ShieldInstance_OnShieldDestroyed(object sender, EventArgs e)
    {
        shieldInstance.gameObject.SetActive(false);
        _curTime = _maxTime;
    }

    private void Update()
    {
        Cooldown();
    }

    public override void HandleActivate()
    {
        if(_curTime <= 0 && CanActivate)
        {
            _isActve = true;
            shieldInstance.gameObject.SetActive(_isActve);
            if (_isActve)
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
            _gadgetManager.Timer(_curTime, _maxTime, _gadgetName);
        }
    }
}
