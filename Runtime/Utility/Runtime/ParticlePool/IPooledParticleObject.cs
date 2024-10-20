namespace Boshphelm.Utility
{
    public interface IPooledParticleObject : IPooledObject
    {
        void Initialize();
        ParticlePool Pool { get; set; }
        ParticleType ParticleType { get; set; }
    }
}
