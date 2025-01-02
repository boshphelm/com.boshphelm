using Boshphelm.Items;

namespace Boshphelm.ItemSlot
{
    public abstract class ItemSlotBase<TItem> where TItem : Item
    {
        public TItem Item { get; protected set; }

        public System.Action<ItemSlotBase<TItem>, TItem> OnItemChanged = (_, _) => { };
        public System.Action<ItemSlotBase<TItem>, TItem> OnItemRemoved = (_, _) => { };

        public virtual void SetItem(TItem item)
        {
            if (item == null) return;
            if (!CanAcceptItem(item)) return;

            Item = item;
            OnItemChanged.Invoke(this, item);
        }

        public virtual TItem RemoveItem()
        {
            var item = Item;
            Item = null;

            OnItemRemoved.Invoke(this, item);
            return item;
        }

        public virtual void ClearSlot()
        {
            RemoveItem();
            OnItemChanged = (_, _) => { };
            OnItemRemoved = (_, _) => { };
        }

        public bool IsEmpty => Item == null;

        public abstract bool CanAcceptItem(TItem item);
    }
}
