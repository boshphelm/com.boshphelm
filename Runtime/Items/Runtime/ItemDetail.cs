using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Items
{
    [CreateAssetMenu(menuName = "Boshphelm/Items/ItemDetail", fileName = "ItemDetail")]
    public class ItemDetail : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public string DisplayName;
        public bool Stackable;

        public Sprite Icon;
        [TextArea] public string Description;

        public ItemType ItemType;

        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 1)]
        private void AssignNewGuid()
        {
            Id = SerializableGuid.NewGuid();
        }

        public Item Create(int quantity) => new Item(this, quantity);
    }
}