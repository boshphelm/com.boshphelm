using UnityEngine;

namespace boshphelm.Utility
{
    public interface IDamageable
    {
        public System.Action<IDamageable> OnDestroying { get; set; }

        bool Dead { get; }
        void GetHit(int damage);
        void ApplyForce(Vector3 force);
    }
}