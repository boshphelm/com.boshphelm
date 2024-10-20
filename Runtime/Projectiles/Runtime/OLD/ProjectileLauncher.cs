using Boshphelm.Projectiles;
using UnityEngine;

namespace Boshphelm.OLD.Projectiles
{
    public abstract class ProjectileLauncher
    {
        private readonly ProjectileType _projectileType;
        private readonly Transform _projectileSpawnPoint;

        public ProjectileLauncher(ProjectileType projectileType, Transform projectileSpawnPoint)
        {
            _projectileType = projectileType;
            _projectileSpawnPoint = projectileSpawnPoint;
        }

        /*public void LaunchProjectile(Unit target, Transform targetTransform, IDamage damage, Unit source)
        {
            var projectileObj = ProjectilePool.Instance.SpawnFromPool(_projectileType, _projectileSpawnPoint);
            projectileObj.Launch(target, targetTransform, damage, source);
        }*/
    }
}
