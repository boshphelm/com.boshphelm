using Boshphelm.Utility;

namespace Boshphelm.Items
{
    [System.Serializable]
    public abstract class Item
    {
        public SerializableGuid Id;
        public SerializableGuid ItemDetailId;
        public ItemDetail ItemDetail;
        public int Quantity;

        protected Item(ItemDetail itemDetail, int quantity = 1)
        {
            Id = SerializableGuid.NewGuid();
            ItemDetail = itemDetail;
            Quantity = ItemDetail.Stackable ? quantity : 1;
        }
    }
}
