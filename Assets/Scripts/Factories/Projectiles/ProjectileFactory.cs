using Data;
using Projectiles;
using Services.Assets;
using Object = UnityEngine.Object;

namespace Factories.Projectiles
{
    public class ProjectileFactory : IProjectileFactory
    {
        private GuidedProjectile _guidedProjectilePrefab;
        private CannonProjectile _cannonProjectilePrefab;

        private readonly IAssetProvider _assetProvider;
        private readonly ProjectilesConfig _cannonProjectileConfig;
        private readonly ProjectilesConfig _guidedProjectileConfig;

        public ProjectileFactory(IAssetProvider assetProvider, ProjectilesConfig cannonProjectileConfig, ProjectilesConfig guidedProjectileConfig)
        {
            _assetProvider = assetProvider;
            _cannonProjectileConfig = cannonProjectileConfig;
            _guidedProjectileConfig = guidedProjectileConfig;
        }

        public GuidedProjectile CreateGuidedProjectile()
        {
            _guidedProjectilePrefab ??= _assetProvider.Load<GuidedProjectile>(AssetPath.GuidedProjectilePrefab);
            GuidedProjectile projectile = Object.Instantiate(_guidedProjectilePrefab);
            projectile.Construct(_guidedProjectileConfig.Damage, _guidedProjectileConfig.MoveSpeed, _guidedProjectileConfig.LifeTime);

            return projectile;
        }

        public CannonProjectile CreateCannonProjectile()
        {
            _cannonProjectilePrefab ??= _assetProvider.Load<CannonProjectile>(AssetPath.CannonProjectilePrefab);
            CannonProjectile projectile = Object.Instantiate(_cannonProjectilePrefab);

            projectile.Construct(_cannonProjectileConfig.Damage, _cannonProjectileConfig.MoveSpeed, _cannonProjectileConfig.LifeTime);

            return projectile;
        }
    }
}