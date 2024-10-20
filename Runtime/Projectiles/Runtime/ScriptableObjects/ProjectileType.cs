using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Projectiles
{
    [CreateAssetMenu(fileName = "New Projectile Type", menuName = "Boshphelm/Projectile System/Projectile Type")]
    public class ProjectileType : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();

        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 1)]
        private void AssignNewGuid()
        {
            Id = SerializableGuid.NewGuid();
        }
    }
}
