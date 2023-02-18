using Projectiles;
using UnityEngine;

namespace Towers
{
    public class SimpleTower : Tower
    {
        [SerializeField] private Transform _shootPoint;

        protected override void Shoot()
        {
            GuidedProjectile projectile = ProjectilePool.GetGuidedProjectile();
            projectile.ResetLifeTime();
            projectile.gameObject.SetActive(true);
            projectile.Exploded += OnProjectileExploded;
            
            projectile.transform.position = _shootPoint.position;
            projectile.Launch(Target);
        }

        private void OnProjectileExploded(Projectile projectile)
        {
            ProjectilePool.ReturnToPool(projectile);
            projectile.Exploded -= OnProjectileExploded;
        }
    }
}