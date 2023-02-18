using Projectiles;
using UnityEngine;

namespace Towers
{
    public class CannonTower : Tower
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _cannonHub;
        [SerializeField] private float _rotationSpeed = 5f;

        protected override void Update()
        {
            base.Update();
            RotateToTarget();
        }

        private void RotateToTarget()
        {
            if (Target == null) return;

            Vector3 direction = GetDirection();
            _cannonHub.rotation = Quaternion.Lerp(_cannonHub.rotation, Quaternion.LookRotation(direction),
                _rotationSpeed * Time.deltaTime);
        }


        protected override void Shoot()
        {
            CannonProjectile projectile = ProjectilePool.GetCannonProjectile();
            projectile.ResetLifeTime();
            
            projectile.gameObject.SetActive(true);
            projectile.Exploded += OnProjectileExploded;

            projectile.transform.position = _shootPoint.position;

            Vector3 hitPoint = GetHitPoint(Target.HitPosition, Target.LastSpeed, transform.position,
                projectile.Speed, out float time);

            Vector3 projectileSpeed = GetProjectileSpeed(hitPoint, time, projectile);

            projectile.Launch(projectileSpeed);
        }

        private void OnProjectileExploded(Projectile projectile)
        {
            ProjectilePool.ReturnToPool(projectile);
            projectile.Exploded -= OnProjectileExploded;
        }

        private Vector3 GetHitPoint(Vector3 targetPosition, Vector3 targetSpeed, Vector3 attackerPosition,
            float bulletSpeed,
            out float time)
        {
            Vector3 q = targetPosition - attackerPosition;
            q.y = 0;
            targetSpeed.y = 0;

            float a = Vector3.Dot(targetSpeed, targetSpeed) - bulletSpeed * bulletSpeed;
            float b = 2 * Vector3.Dot(targetSpeed, q);
            float c = Vector3.Dot(q, q);

            float D = Mathf.Sqrt((b * b) - 4 * a * c);

            float t1 = (-b + D) / (2 * a);
            float t2 = (-b - D) / (2 * a);

            time = Mathf.Max(t1, t2);

            Vector3 ret = targetPosition + targetSpeed * time;
            return ret;
        }

        private Vector3 GetProjectileSpeed(Vector3 hitPoint, float time, CannonProjectile projectile)
        {
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0;

            float antiGravity = -Physics.gravity.y * time / 2;
            float deltaY = (hitPoint.y - projectile.transform.position.y) / time;

            Vector3 arrowSpeed = direction.normalized * projectile.Speed;
            arrowSpeed.y = antiGravity + deltaY;
            return arrowSpeed;
        }
    }
}