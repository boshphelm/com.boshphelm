using UnityEngine;

namespace Boshphelm.Utility
{
    public static class Vector3Extensions
    {
        public static float MagnitudeDistance(this Vector3 a, Vector3 b) => (a - b).magnitude;

        public static float MagnitudeDistanceSquared(this Vector3 a, Vector3 b) => (a - b).sqrMagnitude;
    }
}
