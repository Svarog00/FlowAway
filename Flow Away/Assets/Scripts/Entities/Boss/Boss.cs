using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IHealth
{
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs: EventArgs
    {
        public int currentHealth;
    }
    protected int _healthPoints;

    public ColliderTrigger _colliderTrigger;

    protected GameObject _player;
    protected Player_Health _playerHealth;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _chillTime = 5f;
    protected float chill;
    protected string _currentState;
    [SerializeField] private int _healthPointMax;

    private void Start()
    {
        _healthPoints = _healthPointMax;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { currentHealth = _healthPoints });
    }

    public virtual void Heal()
    {    }

    public virtual void Hurt(int damage)
    {
        _healthPoints -= damage;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { currentHealth = _healthPoints }); //Apply UI;
    }

    protected virtual void ColliderTriger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {}

    protected void ChangeAnimationState(string newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
    }
    
    public int GetHealth()
    {
        return _healthPoints;
    }

    
}
