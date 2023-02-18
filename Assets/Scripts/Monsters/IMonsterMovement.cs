using UnityEngine;

namespace Monsters
{
    public interface IMonsterMovement
    {
        Vector3 Position { get; }
        Vector3 HitPosition { get; }
        Vector3 LastSpeed { get; }
    }
}