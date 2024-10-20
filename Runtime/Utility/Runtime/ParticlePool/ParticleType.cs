using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Utility
{
    [CreateAssetMenu(fileName = "New Particle Type", menuName = "Boshphelm/Particle System/Particle Type")]
    public class ParticleType : ScriptableObject
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
