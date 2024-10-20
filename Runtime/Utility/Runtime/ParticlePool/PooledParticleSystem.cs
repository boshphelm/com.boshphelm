using UnityEngine;

namespace Boshphelm.Utility
{
    public class PooledParticleSystem : MonoBehaviour, IPooledParticleObject
    {
        private ParticleSystem _particleSystem;

        public ParticlePool Pool { get; set; }
        public ParticleType ParticleType { get; set; }
        public Transform Transform => transform;

        private bool _released;

        public void Initialize()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void OnObjectSpawn()
        {
            _particleSystem.Clear();
            _particleSystem.Play();
            _released = false;
        }

        public void ReturnToPool()
        {
            if (_released) return;

            if (Pool != null && ParticleType != null)
            {
                Pool.ReturnToPool(ParticleType, gameObject);
                _released = true;
            }
            else
            {
                Debug.LogWarning("Cannot return to pool: Pool or ParticleType is not set.");
            }
        }
    }
}
