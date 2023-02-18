using Services.Pool.Monsters;
using UnityEngine;

namespace Monsters
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnInterval = 3;
        [SerializeField] private Transform _moveTarget;
    
        private float _lastSpawnTime = -1;
        private IMonsterPool _monsterPool;


        public void Construct(IMonsterPool monsterPool)
        {
            _monsterPool = monsterPool;
        }
    
        private void Update()
        {
            if (Time.time > _lastSpawnTime + _spawnInterval)
            {
                SpawnMonster();

                _lastSpawnTime = Time.time;
            }
        }

        private void SpawnMonster()
        {
            GameObject monster = _monsterPool.GetMonster();
            monster.SetActive(true);
            monster.transform.position = transform.position;
            monster.GetComponent<MonsterMovement>().SetTargetPosition(_moveTarget.position);

            monster.GetComponent<MonsterHealth>().Died += OnMonsterDied;
        }

        private void OnMonsterDied(GameObject monster)
        {
            _monsterPool.ReturnToPool(monster);
            monster.GetComponent<MonsterHealth>().Died -= OnMonsterDied;
        }
    }
}