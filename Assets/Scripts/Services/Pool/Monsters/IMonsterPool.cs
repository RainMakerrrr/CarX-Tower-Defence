using Monsters;
using UnityEngine;

namespace Services.Pool.Monsters
{
    public interface IMonsterPool
    {
        GameObject GetMonster();
        void ReturnToPool(GameObject monsterHealth);
    }
}