using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerShield : Gadget
{
    [SerializeField] private PowerShieldInstance shieldInstance;

    private bool _isActve;

    [SerializeField] private float _maxTime = 0f;
    [SerializeField] private float _curTime = 0f;

    private GameObject _player;

    private void Start()
    {
        CanActivate = QuestValues.Instance.GetStage(gadgetName) > 0 ? true : false;

        _isActve = false;
        _player = gameObject;

        shieldInstance.OnShieldDestroyed += ShieldInstance_OnShieldDestroyed;
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

    public void HandleActivate()
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
            GadgetManager.Timer(_curTime, _maxTime, gadgetName);
        }
    }
}
