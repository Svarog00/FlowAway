using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PowerShieldInstance : MonoBehaviour, IDamagable
{
    public event EventHandler OnShieldDestroyed;

    [SerializeField] private int _maxCapacity = 0;
    [SerializeField] private int _curCapacity;

    void OnEnable()
    {
        _curCapacity = _maxCapacity;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Hurt(int damage)
    {
        if (_curCapacity > 0)
        {
            _curCapacity -= damage;
            if (_curCapacity <= 0)
            {
                OnShieldDestroyed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
