using System;
using System.Collections.Generic;
using Factories.Projectiles;
using Projectiles;

namespace Services.Pool
{
    public class ProjectilePool : IProjectilePool
    {
        private readonly Queue<CannonProjectile> _cannonProjectilesPool = new Queue<CannonProjectile>();
        private readonly Queue<GuidedProjectile> _guidedProjectilesPool = new Queue<GuidedProjectile>();

        private readonly IProjectileFactory _projectileFactory;

        public ProjectilePool(IProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }


        public CannonProjectile GetCannonProjectile()
        {
            if (_cannonProjectilesPool.Count != 0) return _cannonProjectilesPool.Dequeue();

            AddProjectileToPool(ProjectileType.Cannon);

            return _cannonProjectilesPool.Dequeue();
        }

        public GuidedProjectile GetGuidedProjectile()
        {
            if (_guidedProjectilesPool.Count != 0) return _guidedProjectilesPool.Dequeue();

            AddProjectileToPool(ProjectileType.Guided);

            return _guidedProjectilesPool.Dequeue();
        }

        public void ReturnToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);

            switch (projectile)
            {
                case CannonProjectile cannonProjectile:
                    _cannonProjectilesPool.Enqueue(cannonProjectile);
                    break;
                case GuidedProjectile guidedProjectile:
                    _guidedProjectilesPool.Enqueue(guidedProjectile);
                    break;
            }
        }

        private void AddProjectileToPool(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.Cannon:
                    CannonProjectile cannonProjectile = _projectileFactory.CreateCannonProjectile();
                    cannonProjectile.gameObject.SetActive(false);
                    _cannonProjectilesPool.Enqueue(cannonProjectile);
                    break;
                case ProjectileType.Guided:
                    GuidedProjectile guidedProjectile = _projectileFactory.CreateGuidedProjectile();
                    guidedProjectile.gameObject.SetActive(false);
                    _guidedProjectilesPool.Enqueue(guidedProjectile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}