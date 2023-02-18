using UnityEngine;

namespace Projectiles
{
    public class CannonProjectile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void Launch(Vector3 speed)
        {
            _rigidbody.velocity = speed;
        }

        protected override void Move() => transform.forward = _rigidbody.velocity;
    }
}