using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Projectiles
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private ProjectilePool _projectilePool;

        [Header("Projectile Types...")]
        [SerializeField] private ProjectileType _lockOnProjectileType;
        [SerializeField] private ProjectileType _directionalProjectileType;

        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
            _projectilePool.Initialize();
        }

        public void LaunchLockOnProjectile(Transform target)
        {
            var projectile = _projectilePool.SpawnProjectile(_lockOnProjectileType, _myTransform.position, _myTransform.rotation);
            if (projectile is ITargetable targetable)
            {
                targetable.SetTarget(target);
            }
        }

        public void LaunchDirectionalProjectile()
        {
            var projectile = _projectilePool.SpawnProjectile(_directionalProjectileType, _myTransform.position, _myTransform.rotation);
            if (projectile is IDirectional directional)
            {
                directional.SetDirection(_myTransform.forward);
            }
        }
    }
}
