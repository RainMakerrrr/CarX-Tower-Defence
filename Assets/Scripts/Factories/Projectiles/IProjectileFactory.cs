using Projectiles;

namespace Factories.Projectiles
{
    public interface IProjectileFactory
    {
        GuidedProjectile CreateGuidedProjectile();
        CannonProjectile CreateCannonProjectile();
    }
}