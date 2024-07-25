using System.Collections;
using System.Collections.Generic;
using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Items
{
    [CreateAssetMenu(fileName = "ItemType", menuName = "Boshphelm/Items/ItemType")]
    public class ItemType : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public string DisplayName;
        public Sprite Icon;

        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 1)]
        private void AssignNewGuid()
        {
            Id = SerializableGuid.NewGuid();
        }
    }
}