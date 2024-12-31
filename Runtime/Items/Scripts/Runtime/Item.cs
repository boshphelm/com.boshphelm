using Boshphelm.Utility;

namespace Boshphelm.Items
{
    [System.Serializable]
    public abstract class Item<T> where T : ItemDetail
    {
        public SerializableGuid Id;
        public SerializableGuid ItemDetailId;
        public T ItemDetail;
        public int Quantity;

        protected Item(T itemDetail, int quantity = 1)
        {
            Id = SerializableGuid.NewGuid();
            ItemDetail = itemDetail;
            Quantity = quantity;
        }
    }
}
