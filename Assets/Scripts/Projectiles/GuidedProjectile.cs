using Monsters;
using Services;
using UnityEngine;

namespace Projectiles
{
    public class GuidedProjectile : Projectile
    {
        private IMonsterMovement _target;

        public void Launch(IMonsterMovement target)
        {
            _target = target;
        }

        protected override void Move()
        {
            if (_target == null) return;

            Vector3 translation = _target.Position - transform.position;

            transform.Translate(translation.normalized * (Speed * Time.deltaTime));
        }
    }
}