using System;
using Factories.Projectiles;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Projectiles Config", menuName = "Configs/Projectiles Config")]
    public class ProjectilesConfig : ScriptableObject
    {
        [SerializeField] [Range(1, 100)] private int _damage;
        [SerializeField] [Range(10, 100)] private float _moveSpeed;
        [SerializeField] [Range(1, 20)] private float _lifeTime;
        [SerializeField] private ProjectileType _projectileType;

        public int Damage => _damage;

        public float MoveSpeed => _moveSpeed;

        public float LifeTime => _lifeTime;
        
        public ProjectileType ProjectileType => _projectileType;
    }
}