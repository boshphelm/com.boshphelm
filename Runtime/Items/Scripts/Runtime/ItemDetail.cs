using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Items
{
    //[CreateAssetMenu(menuName = "Boshphelm/Items/ItemDetail", fileName = "ItemDetail")]
    public abstract class ItemDetail : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public string DisplayName;
        public bool Stackable;
        public Sprite Icon;
        [TextArea] public string Description;
        public ItemType ItemType;
        //public BaseStatContainer BaseStatContainer;
        //public ItemStat[] ItemStats;
        //public int MaxItemLevel => ItemStats[0].Values.Length;

        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 1)]
        private void AssignNewGuid()
        {
            Id = SerializableGuid.NewGuid();
        }
    }

    [System.Serializable]
    public abstract class ItemDetailQuantity<TItemDetail> where TItemDetail : ItemDetail
    {
        public TItemDetail ItemDetail;
        public int Quantity;
    }
    /*[Serializable]
    public class ItemStat
    {
        public ItemStatSO itemStatSO;
        public float[] Values;
    }

    [Serializable]
    public class ShopUI
    {
        public Vector3 size;
        public Quaternion rotation;
        public Sprite Icon;
    }*/
}
