using UnityEngine;

namespace Boshphelm.Projectiles
{
    public class ProjectileHitLogger : IProjectileObserver
    {
        public void OnProjectileHit(IProjectile projectile, GameObject target)
        {
            Debug.Log("PROJECTILE : " + projectile.GameObject + ", HIT TO TARGET : " + target, projectile.GameObject);
        }
    }
}
