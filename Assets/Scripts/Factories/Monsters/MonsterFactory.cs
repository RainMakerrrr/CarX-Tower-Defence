using Monsters;
using Services.Assets;
using UnityEngine;

namespace Factories.Monsters
{
    public class MonsterFactory : IMonsterFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly int _health;
        private readonly float _moveSpeed;

        private GameObject _monsterHealthPrefab;

        public MonsterFactory(IAssetProvider assetProvider, int health, float moveSpeed)
        {
            _assetProvider = assetProvider;
            _health = health;
            _moveSpeed = moveSpeed;
        }

        public GameObject Create()
        {
            _monsterHealthPrefab ??= _assetProvider.Load<GameObject>(AssetPath.MonsterPrefab);
            GameObject monster = Object.Instantiate(_monsterHealthPrefab);
            monster.GetComponent<MonsterHealth>().Construct(_health);
            monster.GetComponent<MonsterMovement>().Construct(_moveSpeed);

            return monster;
        }
    }
}