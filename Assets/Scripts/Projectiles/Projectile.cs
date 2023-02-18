using System;
using Monsters;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public event Action<Projectile> Exploded; 

        public float Speed => _speed;
        private bool LifeTimeExpired => _lifeTime < 0;

        private float _startLifeTime;
        private float _speed;
        private int _damage;
        private float _lifeTime;
        public void Construct(int damage, float speed, float lifeTime)
        {
            _damage = damage;
            _speed = speed;
            _startLifeTime = lifeTime;
        }

        public void ResetLifeTime() => _lifeTime = _startLifeTime;
        
        protected abstract void Move();

        private void Update()
        {
            TickLifeTimer();
            Move();
        }

        private void TickLifeTimer()
        {
            _lifeTime -= Time.deltaTime;

            if (LifeTimeExpired)
            {
                Exploded?.Invoke(this);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<IDamageable>();

            if (monster != null)
            {
                monster.TakeDamage(_damage);
                Exploded?.Invoke(this);
            }
        }
    }
}