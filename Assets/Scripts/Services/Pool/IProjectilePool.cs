using Projectiles;

namespace Services.Pool
{
    public interface IProjectilePool
    {
        CannonProjectile GetCannonProjectile();
        GuidedProjectile GetGuidedProjectile();
        void ReturnToPool(Projectile projectile);
    }
}