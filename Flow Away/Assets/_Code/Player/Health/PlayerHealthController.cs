using Assets.Scripts.Player.Health;
using System;
using UnityEngine;

using CustomEventArguments;

public class PlayerHealthController : MonoBehaviour, IDamagable, IHealable
{
    public event EventHandler OnPlayerDeath;

    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;

    public int MaxHealth => _playerHealth.MaxHealth;
    public int CurrentHealth
    {
        get => _playerHealth.CurrentHealth;
        set
        {
            _playerHealth.CurrentHealth = value;
            if (_playerHealth.CurrentHealth > 0)
            {
                gameObject.SetActive(true);
                OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { CurHealth = _playerHealth.CurrentHealth });
            }
        }
    }

    public int FreeSlots => _freeEnemySlots;

    private PlayerHealthModel _playerHealth;

    [SerializeField] private int _maxEnemySlots;
    private int _freeEnemySlots;

    private void Awake()
    {
        _playerHealth = new PlayerHealthModel();
        _playerHealth.OnDeath += _playerHealth_OnDeath;

        _freeEnemySlots = _maxEnemySlots;
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { CurHealth = _playerHealth.CurrentHealth });
    }

    public void Heal()
    {
        _playerHealth.Heal();
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { CurHealth = _playerHealth.CurrentHealth });
    }

    public void Hurt(int damage)
    {
        CameraShake.Instance.ShakeCamera(2f, .1f);
        _playerHealth.Hurt(damage);
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { CurHealth = _playerHealth.CurrentHealth });
    }

    public void RestoreSlots(int lostSlots)
    {
        _freeEnemySlots += lostSlots;
    }

    public void DecreaseSlots(int weight)
    {
        _freeEnemySlots -= weight;
    }

    private void _playerHealth_OnDeath(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
}
