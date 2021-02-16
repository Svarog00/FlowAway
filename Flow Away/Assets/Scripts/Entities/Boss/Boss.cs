using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IHealth
{
    // Start is called before the first frame update
    [SerializeField] protected int healthPoints;

    public event EventHandler OnBossDamaged;

    public virtual void Heal()
    {    }

    public virtual void Hurt(int damage)
    {
        healthPoints -= damage;
        OnBossDamaged?.Invoke(this, EventArgs.Empty);
        //Apply UI;
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    
}
