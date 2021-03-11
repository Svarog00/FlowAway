using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour, IHealable, IDamagable
{
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs
    {
        public int curHealth;
    }
    public event EventHandler OnDeath;

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;

    private int freeSlots;
    private int slots;

    public int FreeSlots
    {
        get { return freeSlots; }
        set { freeSlots = value; }
    }
    public int CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value;
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = currentHealth });
        }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        slots = 5;
        freeSlots = slots;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = currentHealth });
    }

    public void RestoreSlots(int lostSlots)
    {
        freeSlots += slots - freeSlots;
    }

    public void Heal()
    {
        FindObjectOfType<AudioManager>().Play("Heal");
        currentHealth += maxHealth - currentHealth;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = currentHealth });
    }

    public void Hurt(int damage)
    {
        CameraShake.Instance.ShakeCamera(2f, .1f);
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        currentHealth -= damage;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { curHealth = currentHealth });

        if (currentHealth <= 0)
        {
            //death animation and deleting object
            OnDeath?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
            //death screen mb
        }
    }
}