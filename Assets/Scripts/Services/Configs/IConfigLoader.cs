using System.Collections.Generic;
using Data;
using Factories.Projectiles;

namespace Services.Configs
{
    public interface IConfigLoader
    {
        ProjectilesConfig LoadProjectileConfig(ProjectileType type);
        MonsterConfig LoadMonsterConfig();
        Dictionary<ProjectileType, ProjectilesConfig> LoadAllProjectilesConfig();
    }
}