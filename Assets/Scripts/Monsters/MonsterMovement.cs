using UnityEngine;

namespace Monsters
{
    public class MonsterMovement : MonoBehaviour, IMonsterMovement
    {
        [SerializeField] private float _distanceToTarget = 0.5f;
        [SerializeField] private float _reachDistance;
        [SerializeField] private Transform _hitPosition;

        public Vector3 Position => transform.position;

        public Vector3 HitPosition => _hitPosition.position;

        public Vector3 LastSpeed { get; private set; }

        private Vector3 _targetPosition;
        private float _moveSpeed;

        public void Construct(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        private void Update()
        {
            if (TryDestroy()) return;

            Move();
        }

        private void Move()
        {
            Vector3 translation = _targetPosition - transform.position;

            if (translation.magnitude > _distanceToTarget)
            {
                LastSpeed = translation.normalized * _moveSpeed;
                transform.position += LastSpeed * Time.deltaTime;
                transform.forward = LastSpeed;
            }
            else
            {
                LastSpeed = Vector3.zero;
            }
        }

        private bool TryDestroy()
        {
            if (Vector3.Distance(transform.position, _targetPosition) <= _reachDistance)
            {
                Destroy(gameObject);
                return true;
            }

            return false;
        }
    }
}