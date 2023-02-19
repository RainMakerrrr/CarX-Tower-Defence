using System.Collections.Generic;
using System.Linq;
using Data;
using Factories.Projectiles;
using Services.Assets;

namespace Services.Configs
{
    public class ConfigLoader : IConfigLoader
    {
        private readonly IAssetProvider _assetProvider;

        public ConfigLoader(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public Dictionary<ProjectileType, ProjectilesConfig> LoadAllProjectilesConfig() => _assetProvider
            .LoadAll<ProjectilesConfig>(AssetPath.ProjectilesConfigs).ToDictionary(x => x.ProjectileType);

        public ProjectilesConfig LoadProjectileConfig(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.Cannon:
                    return _assetProvider.Load<ProjectilesConfig>(AssetPath.CannonProjectileConfig);
                case ProjectileType.Guided:
                    return _assetProvider.Load<ProjectilesConfig>(AssetPath.GuidedProjectileConfig);
            }

            return null;
        }

        public MonsterConfig LoadMonsterConfig() => _assetProvider.Load<MonsterConfig>(AssetPath.MonsterConfig);
    }
}