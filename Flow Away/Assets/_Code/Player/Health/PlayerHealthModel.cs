using System;

namespace Assets.Scripts.Player.Health
{
    public class PlayerHealthModel
    {
        public event EventHandler OnDeath;

        private int _maxHealth = 100;
        private int _currentHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public int MaxHealth => _maxHealth;

        public PlayerHealthModel()
        {
            _currentHealth = _maxHealth;
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
}