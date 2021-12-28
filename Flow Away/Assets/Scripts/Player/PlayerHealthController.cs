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

    public event EventHandler<OnCapsulesCountChangedEventArgs> OnCapsulesCountChanged;
    public class OnCapsulesCountChangedEventArgs : EventArgs
    {
        public enum OperationType { Add, Remove }

        public OperationType OperType;
        public int CapsulesCount;
    }
    
    public int PlayerHealth
    {
        get => _playerHealth.CurrentHealth;
        set => _playerHealth.SetHealth(value);
    }

    private PlayerHealthModel _playerHealth;
    private HealingCapsulesModel _capsulesModel;
    
    [SerializeField] private int _initCapsuleCount;

    private void Start()
    {
        _playerHealth = new PlayerHealthModel();
        _capsulesModel = new HealingCapsulesModel(_initCapsuleCount);

        _playerHealth.OnDeath += _playerHealth_OnDeath;

        OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs { 
            OperType = OnCapsulesCountChangedEventArgs.OperationType.Add, 
            CapsulesCount = _initCapsuleCount 
        });
    }

    private void _playerHealth_OnDeath(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }

    public int GetCapsuleCount() => _capsulesModel.Count;

    public void AddCapsule() 
    {
        _capsulesModel.Count++;

        OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs
        {
            OperType = OnCapsulesCountChangedEventArgs.OperationType.Add,
            CapsulesCount = _capsulesModel.Count
        });
    }

    public void LoadCapsule(int count)
    {
        _capsulesModel.Count = count;
        OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs { 
            OperType = OnCapsulesCountChangedEventArgs.OperationType.Add, 
            CapsulesCount = count 
        });
    }

    public void HandleHeal()
    {
        if (_capsulesModel.Count > 0 && _playerHealth.CurrentHealth < _playerHealth.MaxHealth)
        {
            AudioManager.Instance.Play("Heal");

            _playerHealth.Heal();
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = _playerHealth.CurrentHealth });

            OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs {
                OperType = OnCapsulesCountChangedEventArgs.OperationType.Remove,
                CapsulesCount = _capsulesModel.Count
            });

            _capsulesModel.Count--;
        }
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
