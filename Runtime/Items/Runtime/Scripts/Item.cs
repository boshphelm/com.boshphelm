using Boshphelm.Utility;

namespace Boshphelm.Items
{
    [System.Serializable]
    public class Item
    {
        public SerializableGuid Id;
        public SerializableGuid ItemDetailId;
        public ItemDetail ItemDetail;
        public int Quantity;
        public int ItemLevel;

        public Item(ItemDetail itemDetail, int quantity = 1, int itemLevel = 0)
        {
            Id = SerializableGuid.NewGuid();
            ItemDetail = itemDetail;
            ItemDetailId = ItemDetail.Id;
            Quantity = quantity;
            ItemLevel = itemLevel;
        }
    }
}