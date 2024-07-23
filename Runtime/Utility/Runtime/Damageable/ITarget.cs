using UnityEngine;

namespace Boshphelm.Utility
{
    public interface ITarget : IDamageable
    {
        GameObject GameObject { get; }
        Transform FeetPoint { get; }
    }
}