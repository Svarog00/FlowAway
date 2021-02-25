using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IHealth
{
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public event EventHandler<OnBossActivatedUIEventArgs> OnBossActivatedUI;
    public event EventHandler<OnPhaseIconChangeEventArgs> OnBossPhaseIconChange;
    public class OnHealthChangedEventArgs: EventArgs
    {
        public int currentHealth;
    }

    public class OnBossActivatedUIEventArgs : EventArgs
    {
        public bool setUIFalse;
    }

    public class OnPhaseIconChangeEventArgs: EventArgs
    {
        public Sprite changedSprite;
    }

    [SerializeField] protected int _healthPoints;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _chillTime = 5f;
    
    protected GameObject _player;
    protected Player_Health _playerHealth;
    protected float chill;
    protected string _currentState;

    public ColliderTrigger _colliderTrigger;
    public int healthPointMax;

    public virtual void Heal()
    {    }

    public virtual void Hurt(int damage)
    {
        _healthPoints -= damage;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { currentHealth = _healthPoints }); //Apply UI;
    }

    protected void OnActivatedUI(bool setUIBool)
    {
        OnBossActivatedUI?.Invoke(this, new OnBossActivatedUIEventArgs { setUIFalse = setUIBool }); //событие - при входе в ColliderTrigger включается UI босса
    }

    protected void OnPhaseIconChange(Sprite sprite)
    {
        OnBossPhaseIconChange?.Invoke(this, new OnPhaseIconChangeEventArgs { changedSprite = sprite });  //событие - при переключении фазы босса изменяется также иконка фазы в UI
    }


    protected virtual void ColliderTriger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {  }

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
