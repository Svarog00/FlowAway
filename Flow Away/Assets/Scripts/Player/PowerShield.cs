using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerShield : MonoBehaviour, IDamagable
{
    public event EventHandler OnShieldDestroyed;

    private bool _isActve;

    private int _curCapacity;
    [SerializeField] private int _maxCapacity = 0;

    [SerializeField] private GameObject _shieldVisual = null;
    private GameObject _player;

    private void Awake()
    {
        HandlePowerShield handlePowerShield = GetComponentInParent<HandlePowerShield>();
        handlePowerShield.OnShieldActivated += HandlePowerShield_OnShieldActivated;
        _isActve = false;
        _player = GetComponentInParent<Player_Movement>().gameObject;
    }

    private void HandlePowerShield_OnShieldActivated(object sender, HandlePowerShield.OnShieldActivatedEventArgs e)
    {
        _isActve = e.isAcive;
        _shieldVisual.SetActive(_isActve);
        if (_isActve)
        {
            _curCapacity = _maxCapacity;
            gameObject.transform.position = _player.transform.position;
        }
    }

    public void Hurt(int damage)
    {
        if(_isActve && _curCapacity > 0)
        {
            _curCapacity -= damage;
            if(_curCapacity <= 0)
            {
                _isActve = !_isActve;
                _shieldVisual.SetActive(false);
                OnShieldDestroyed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
