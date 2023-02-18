using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Monster Config", menuName = "Configs/Monster Config")]
    public class MonsterConfig : ScriptableObject
    {
        [SerializeField] [Range(1, 200)] private int _health;
        [SerializeField] [Range(1, 20)] private float _moveSpeed;

        public int Health => _health;

        public float MoveSpeed => _moveSpeed;
    }
}