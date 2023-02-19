using System.Collections.Generic;
using Data;
using Factories.Monsters;
using Factories.Projectiles;
using Monsters;
using Services.Assets;
using Services.Configs;
using Services.Pool;
using Services.Pool.Monsters;
using Towers;
using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private Tower[] _towers;
    [SerializeField] private MonsterSpawner _monsterSpawner;

    private IAssetProvider _assetProvider;
    private IProjectileFactory _projectileFactory;
    private IProjectilePool _projectilePool;
    private IMonsterFactory _monsterFactory;
    private IMonsterPool _monsterPool;
    private IConfigLoader _configLoader;

    private Dictionary<ProjectileType, ProjectilesConfig> _projectilesConfigs;
    private MonsterConfig _monsterConfig;

    private void Start()
    {
        InitAssetProvider();
        LoadConfigs();

        InitProjectileFactory();
        InitProjectilePool();

        InitTowers();

        InitMonsterFactory();
        InitMonsterPool();
        InitMonsterSpawner();
    }


    private void InitAssetProvider() => _assetProvider = new AssetProvider();

    private void LoadConfigs()
    {
        _configLoader = new ConfigLoader(_assetProvider);
        _monsterConfig = _configLoader.LoadMonsterConfig();
        _projectilesConfigs = _configLoader.LoadAllProjectilesConfig();
    }

    private void InitProjectileFactory() =>
        _projectileFactory = new ProjectileFactory(_assetProvider, _projectilesConfigs[ProjectileType.Cannon],
            _projectilesConfigs[ProjectileType.Guided]);


    private void InitProjectilePool() => _projectilePool = new ProjectilePool(_projectileFactory);

    private void InitTowers()
    {
        foreach (Tower tower in _towers)
        {
            tower.Construct(_projectilePool);
        }
    }

    private void InitMonsterFactory() => _monsterFactory =
        new MonsterFactory(_assetProvider, _monsterConfig.Health, _monsterConfig.MoveSpeed);

    private void InitMonsterPool() => _monsterPool = new MonsterPool(_monsterFactory);

    private void InitMonsterSpawner() => _monsterSpawner.Construct(_monsterPool);
}