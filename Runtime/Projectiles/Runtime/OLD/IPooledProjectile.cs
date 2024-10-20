using Boshphelm.Projectiles;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.OLD.Projectiles
{
    public interface IPooledProjectile : IPooledObject
    {
        void Initialize();
        void Launch(Transform targetTransform, int penetrationCount = 0);
        Transform Transform { get; }
        GameObject GameObject { get; }
        ProjectilePool Pool { get; set; }
        ProjectileType ProjectileType { get; set; }
        int PenetrationCount { get; }
        event System.Action<IPooledProjectile> OnProjectileHit;
    }
}
