using UnityEngine;

namespace Boshphelm.Projectiles
{
    public interface IProjectileObserver
    {
        void OnProjectileHit(IProjectile projectile, GameObject target);
    }
}
