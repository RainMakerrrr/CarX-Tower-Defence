using System.Linq;
using Data;
using Factories.Monsters;
using Factories.Projectiles;
using Monsters;
using Services.Assets;
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

    private ProjectilesConfig[] _projectilesConfigs;
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
        _projectilesConfigs = _assetProvider.LoadAll<ProjectilesConfig>(AssetPath.ProjectilesConfigs);
        _monsterConfig = _assetProvider.Load<MonsterConfig>(AssetPath.MonsterConfig);
    }

    private void InitProjectileFactory() =>
        _projectileFactory = new ProjectileFactory(_assetProvider,
            _projectilesConfigs.First(config => config.ProjectileType == ProjectileType.Cannon),
            _projectilesConfigs.First(config => config.ProjectileType == ProjectileType.Guided));


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