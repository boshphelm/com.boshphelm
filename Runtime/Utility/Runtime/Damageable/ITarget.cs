using UnityEngine;

namespace boshphelm.Utility
{
    public interface ITarget : IDamageable
    {
        GameObject GameObject { get; }
        Transform FeetPoint { get; }
    }
}