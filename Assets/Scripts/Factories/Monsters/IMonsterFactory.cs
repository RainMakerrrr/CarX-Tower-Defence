using Monsters;
using UnityEngine;

namespace Factories.Monsters
{
    public interface IMonsterFactory
    {
        GameObject Create();
    }
}