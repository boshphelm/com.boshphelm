using UnityEngine;

namespace Boshphelm.Projectiles
{
    public interface IProjectile
    {
        void Initialize(IProjectilePool pool);
        void Launch(Vector3 position, Quaternion rotation);
        void Update();
        void OnHit(GameObject target);
        void ReturnToPool();
        GameObject GameObject { get; }
        Transform Transform { get; }
        ProjectileType ProjectileType { get; }
    }
}
