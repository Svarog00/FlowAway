using System;

public class PlayerHealthModel
{

    public event EventHandler OnDeath;

    private int _maxHealth = 100;
    private int _slots = 5;
    private int _currentHealth;

    private int _freeSlots;

    public int FreeSlots
    {
        get => _freeSlots;
    }

    public int CurrentHealth
    {
        get => _currentHealth; 
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public PlayerHealthModel()
    {
        _currentHealth = _maxHealth;
        _freeSlots = _slots;
    }

    public void SetHealth(int health)
    {
        _currentHealth = health;
    }

    public void RestoreSlots(int lostSlots)
    {
        _freeSlots += _slots - _freeSlots;
    }

    public void DecreaseSlots(int weight)
    {
        _freeSlots -= weight;
    }

    public void Heal()
    {
        _currentHealth += _maxHealth - _currentHealth;
    }

    public void Hurt(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            //death animation and deleting object
            OnDeath?.Invoke(this, EventArgs.Empty);
            //death screen mb
        }
    }
}