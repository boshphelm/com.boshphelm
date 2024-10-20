using UnityEngine;

namespace Boshphelm.Projectiles
{
    [System.Serializable]
    public class ProjectilePoolItem
    {
        public ProjectileType Type;
        public GameObject Prefab;
        public int DefaultCapacity = 10;
        public int MaxSize = 20;
    }
}
