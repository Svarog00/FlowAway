using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamagable
{
    public event EventHandler OnPlayerDeath;

    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs
    {
        public int curHealth;
    }

    public int MaxHealth => _playerHealth.MaxHealth;
    public int CurrentHealth
    {
        get => _playerHealth.CurrentHealth;
        set => _playerHealth.CurrentHealth = value;
    }

    private PlayerHealthModel _playerHealth;

    private void Start()
    {
        _playerHealth = new PlayerHealthModel();

        _playerHealth.OnDeath += _playerHealth_OnDeath;

        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = _playerHealth.CurrentHealth });
    }

    private void _playerHealth_OnDeath(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }

    public void Heal()
    {
        _playerHealth.Heal();
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = _playerHealth.CurrentHealth });
    }

    public void Hurt(int damage)
    {
        CameraShake.Instance.ShakeCamera(2f, .1f);
        _playerHealth.Hurt(damage);
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = _playerHealth.CurrentHealth });
    }

    public void RestoreSlots(int lostSlots)
    {
        _playerHealth.RestoreSlots(lostSlots);
    }

    public void DecreaseSlots(int weight)
    {
        _playerHealth.DecreaseSlots(weight);
    }

    public int GetFreeSlots() => _playerHealth.FreeSlots;
}
