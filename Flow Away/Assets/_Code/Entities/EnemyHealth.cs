using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IPoolable, IDamagable
{
    private const string HurtSoundName = "EnemyHurt";

    public EncounterManager eventSource;
    public event EventHandler OnZeroHealth;

    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;

    [SerializeField] private int _hpMax;
    private int _currentHealth;
    protected ObjectPool objectPool;

    private void Start()
    {
        _currentHealth = _hpMax;
    }

    public void Hurt(int damage) //get damage from player or another entity
    {
        FindObjectOfType<AudioManager>().Play(HurtSoundName);
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Death();
        }
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { CurHealth = _currentHealth });
    }

    private void Death()
    {
        OnZeroHealth?.Invoke(this, EventArgs.Empty);
        Count();

        if (objectPool != null)
        {
            ReturnToPool();
        }
        else
        {
            Destroy(gameObject, 0.5f);

        }
    }

    public void SetPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void ReturnToPool()
    {
        objectPool?.AddToPool(gameObject);
    }

    protected void Count()
    {
        if (eventSource != null)
            eventSource.CurrentEnemyCount++;
    }
}

