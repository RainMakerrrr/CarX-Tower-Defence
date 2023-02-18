using System.Collections.Generic;
using Factories.Monsters;
using UnityEngine;

namespace Services.Pool.Monsters
{
    public class MonsterPool : IMonsterPool
    {
        private readonly Queue<GameObject> _monstersPool = new Queue<GameObject>();
        private readonly IMonsterFactory _monsterFactory;

        public MonsterPool(IMonsterFactory monsterFactory)
        {
            _monsterFactory = monsterFactory;
        }

        public GameObject GetMonster()
        {
            if (_monstersPool.Count != 0) return _monstersPool.Dequeue();
            
            AddMonsterToPool();

            return _monstersPool.Dequeue();
        }
        
        public void ReturnToPool(GameObject monsterHealth)
        {
            monsterHealth.gameObject.SetActive(false);
            _monstersPool.Enqueue(monsterHealth);
        }

        private void AddMonsterToPool()
        {
            GameObject monsterHealth = _monsterFactory.Create();
            monsterHealth.gameObject.SetActive(false);
            _monstersPool.Enqueue(monsterHealth);
        }
    }
}