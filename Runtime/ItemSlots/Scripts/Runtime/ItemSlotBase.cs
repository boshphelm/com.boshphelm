using Boshphelm.Items;

namespace Boshphelm.ItemSlot
{
    public abstract class ItemSlotBase<TItem, TItemDetail>
        where TItem : Item
        where TItemDetail : ItemDetail
    {
        public TItem Item { get; protected set; }

        public System.Action<TItem> OnItemChanged = _ => { };
        public System.Action<TItem> OnItemRemoved = _ => { };

        public virtual void SetItem(TItem item)
        {
            if (item == null) return;
            if (!CanAcceptItem(item)) return;

            Item = item;
            OnItemChanged.Invoke(item);
        }

        public virtual TItem RemoveItem()
        {
            var item = Item;
            Item = null;

            OnItemRemoved.Invoke(item);
            return item;
        }

        public bool IsEmpty => Item == null;

        public abstract bool CanAcceptItem(TItem item);
    }
}
