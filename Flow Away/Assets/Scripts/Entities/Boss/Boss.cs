using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IHealth
{
    // Start is called before the first frame update
    [SerializeField] protected int healthPoints;
    [SerializeField] protected ColliderTrigger _colliderTrigger;

    
    protected GameObject _player;
    protected Player_Health _playerHealth;
    [SerializeField] protected float _chillTime = 5f;
    protected float chill;
    public int damage;
    protected string _currentState;

    public event EventHandler OnBossDamaged;

    public virtual void Heal()
    {    }

    public virtual void Hurt(int damage)
    {
        healthPoints -= damage;
        OnBossDamaged?.Invoke(this, EventArgs.Empty);
        //Apply UI;
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
        return healthPoints;
    }

    
}
