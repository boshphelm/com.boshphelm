using System.Collections.Generic;
using System.Linq;
using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.ItemSlot
{
    public abstract class ItemSlotManagerBase<TItem, TItemSlot>
        where TItem : Item
        where TItemSlot : ItemSlotBase<TItem>
    {
        protected readonly List<TItemSlot> itemSlots = new List<TItemSlot>();

        public int SlotCount => itemSlots.Count;

        protected virtual void RegisterSlotEvents(TItemSlot itemSlot)
        {
            itemSlot.OnItemChanged += OnSlotItemChanged;
            itemSlot.OnItemRemoved += OnSlotItemRemoved;
        }

        private void OnDestroy()
        {
            foreach (var slot in itemSlots)
            {
                UnregisterSlotEvents(slot);
            }
            itemSlots.Clear();
        }

        protected virtual void UnregisterSlotEvents(TItemSlot itemSlot)
        {
            itemSlot.OnItemChanged -= OnSlotItemChanged;
            itemSlot.OnItemRemoved -= OnSlotItemRemoved;
        }

        protected abstract void OnSlotItemChanged(ItemSlotBase<TItem> itemSlot, TItem newItem);
        protected abstract void OnSlotItemRemoved(ItemSlotBase<TItem> itemSlot, TItem removedItem);

        public virtual bool HasEmptySlot()
        {
            return itemSlots.Any(slot => slot.IsEmpty);
        }

        public virtual TItemSlot GetEmptySlot()
        {
            return itemSlots.FirstOrDefault(slot => slot.IsEmpty);
        }

        public virtual TItemSlot GetSlotByItem(TItem item)
        {
            return itemSlots.FirstOrDefault(slot => !slot.IsEmpty && slot.Item.Id == item.Id);
        }

        public virtual TItemSlot GetSlotByItemDetail(ItemDetail itemDetail)
        {
            return itemSlots.FirstOrDefault(slot => !slot.IsEmpty && slot.Item.ItemDetail.Id == itemDetail.Id);
        }

        public virtual List<TItemSlot> GetSlotsByItemDetail(ItemDetail itemDetail)
        {
            return itemSlots.Where(slot => !slot.IsEmpty && slot.Item.ItemDetail.Id == itemDetail.Id).ToList();
        }

        public virtual bool TryAddItem(TItem item)
        {
            var emptySlot = GetEmptySlot();
            if (emptySlot == null) return false;

            if (!emptySlot.CanAcceptItem(item)) return false;

            emptySlot.SetItem(item);
            return true;
        }

        public virtual bool TryRemoveItem(TItem item)
        {
            var slot = GetSlotByItem(item);
            if (slot == null) return false;

            slot.RemoveItem();
            return true;
        }
    }
}
