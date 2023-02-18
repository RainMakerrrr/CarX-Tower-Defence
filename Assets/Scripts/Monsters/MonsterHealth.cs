using System;
using UnityEngine;

namespace Monsters
{
    public class MonsterHealth : MonoBehaviour, IDamageable
    {
        public event Action<GameObject> Died; 

        public bool IsDead => _currentHealth <= 0;

        private int _startHealth;
        private int _currentHealth;

        public void Construct(int startHealth)
        {
            _startHealth = startHealth;
            _currentHealth = _startHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (IsDead)
                Die();
        }

        private void Die()
        {
            Died?.Invoke(gameObject);
            _currentHealth = _startHealth;
        }
    }
}