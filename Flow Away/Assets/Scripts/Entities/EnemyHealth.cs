using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IPoolable, IDamagable
{
    public EventManager eventSource;
    public event EventHandler OnZeroHealth;

    [SerializeField] private int _hpMax;
    [SerializeField] private int _hp;
    protected ObjectPool objectPool;

    public virtual void Hurt(int damage) //get damage from player or another entity
    {
        FindObjectOfType<AudioManager>().Play("EnemyHurt");
        _hp -= damage;
        if (_hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<AudioManager>().Play(this.ToString());
        OnZeroHealth?.Invoke(this, EventArgs.Empty);
        Count();

        if (objectPool != null)
            ReturnToPool();
        else Destroy(gameObject, 0.5f);
    }

    public void SetPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void ReturnToPool()
    {
        objectPool.AddToPool(gameObject);
    }

    protected void Count()
    {
        if (eventSource != null)
            eventSource.CurrentEnemyCount++;
    }
}

