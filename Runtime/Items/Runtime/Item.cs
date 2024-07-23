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

        public Item(ItemDetail ıtemDetail, int quantity = 1)
        {
            Id = SerializableGuid.NewGuid();

            ItemDetail = ıtemDetail;
            ItemDetailId = ItemDetail.Id;

            Quantity = quantity;
        }
    }
}