using System.Collections.Generic;
using System.Linq;
using Monsters;
using Services.Pool;
using UnityEngine;

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        private const string MonsterLayerName = "Monster";

        [SerializeField] private float _detectRadius = 5f;
        [SerializeField] private float _attackRange = 10f;
        [SerializeField] private float _attackDelay = 2f;

        private readonly Collider[] _colliders = new Collider[5];
        private IMonsterMovement _target;

        private float _attackTimer;

        protected IProjectilePool ProjectilePool { get; private set; }
        protected IMonsterMovement Target => _target;

        public void Construct(IProjectilePool projectilePool)
        {
            ProjectilePool = projectilePool;
            _attackTimer = 0f;
        }

        protected abstract void Shoot();

        protected virtual void Update()
        {
            if (IsAcquireTarget(out _target))
            {
                _attackTimer -= Time.deltaTime;

                Vector3 direction = GetDirection();

                if (_attackTimer < 0 && direction.magnitude < _attackRange)
                {
                    Shoot();
                    ResetTime();
                }
            }
        }

        protected Vector3 GetDirection()
        {
            Vector3 direction = _target.Position - transform.position;
            direction.y = 0f;
            return direction;
        }

        private void ResetTime() => _attackTimer = _attackDelay;


        private bool IsAcquireTarget(out IMonsterMovement target)
        {
            if (IsFindEnemiesInRange())
            {
                List<IMonsterMovement> sortedColliders = SortEnemiesByDistance();

                target = sortedColliders[0];
                return true;
            }

            target = null;
            return false;
        }

        private List<IMonsterMovement> SortEnemiesByDistance() =>
            _colliders.Where(c => c != null)
                .Select(c => c.GetComponent<IMonsterMovement>())
                .OrderBy(monster => Vector3.Distance(monster.Position, transform.position)).ToList();

        private bool IsFindEnemiesInRange()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, _detectRadius, _colliders,
                LayerMask.GetMask(MonsterLayerName));

            return count > 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}