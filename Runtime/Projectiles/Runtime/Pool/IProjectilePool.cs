namespace Boshphelm.Projectiles
{
    public interface IProjectilePool
    {
        void ReturnToPool(IProjectile projectile);
    }
}
