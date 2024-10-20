using UnityEngine;

namespace Boshphelm.Utility
{
    public interface IPooledObject
    {
        Transform Transform { get; }
        void OnObjectSpawn();
        void ReturnToPool();
    }
}
