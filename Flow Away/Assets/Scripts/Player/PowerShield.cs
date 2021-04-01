using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour, IDamagable
{
    public float time;

    [SerializeField] private int _capacity;
    [SerializeField] private float _maxTime;
    private bool _isActve;
    private float _curTime;
    [SerializeField] private GameObject _shield;

    private void Start()
    {
        HandlePowerShield handlePowerShield = GetComponentInParent<HandlePowerShield>();
        handlePowerShield.OnShieldActivated += HandlePowerShield_OnShieldActivated;
        _isActve = false;
    }

    private void HandlePowerShield_OnShieldActivated(object sender, HandlePowerShield.OnShieldActivatedEventArgs e)
    {
        _isActve = e.isAcive;
    }

    private void Update()
    {

    }

    public void Hurt(int damage)
    {
        _capacity -= damage;
    }
}
