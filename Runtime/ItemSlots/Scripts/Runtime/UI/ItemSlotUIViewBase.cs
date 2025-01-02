using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.ItemSlot
{
    public abstract class ItemSlotUIViewBase<TItem, TItemSlot> : MonoBehaviour
        where TItem : Item
        where TItemSlot : ItemSlotBase<TItem>
    {
        protected TItemSlot itemSlot;

        public virtual void Initialize(TItemSlot slot)
        {
            itemSlot = slot;
            itemSlot.OnItemChanged += UpdateUI;
            itemSlot.OnItemRemoved += HandleItemRemoved;

            UpdateUI(itemSlot, itemSlot.Item);
        }

        protected abstract void UpdateUI(ItemSlotBase<TItem> slot, TItem item);
        protected virtual void HandleItemRemoved(ItemSlotBase<TItem> slot, TItem item)
        {
            ClearUI();
        }

        protected abstract void ClearUI();

        private void OnDestroy()
        {
            if (itemSlot != null)
            {
                itemSlot.OnItemChanged -= UpdateUI;
                itemSlot.OnItemRemoved -= HandleItemRemoved;
            }
        }

        public abstract void OnSelected();
    }
}
