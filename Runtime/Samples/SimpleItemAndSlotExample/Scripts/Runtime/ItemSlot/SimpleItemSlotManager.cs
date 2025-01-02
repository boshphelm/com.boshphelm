using System;
using Boshphelm.Items;
using Boshphelm.ItemSlot;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemSlotManager : ItemSlotManagerBase<SimpleItem, SimpleItemSlot>
    {
        private readonly SimpleItemSlotPool _simpleItemSlotPool;
        public SimpleItemSlotManager(int initialSlotCapacity, int maxSlotCapacity)
        {
            _simpleItemSlotPool = new SimpleItemSlotPool(initialSlotCapacity, maxSlotCapacity);
        }

        public SimpleItemSlot CreateSlot() => _simpleItemSlotPool.GetFromPool();
        public void RemoveSlot(SimpleItemSlot slot) => _simpleItemSlotPool.ReturnToPool(slot);

        /*public void MoveItemFromSlotToInventory(SimpleItemSlot itemSlot)
        {
            if (itemSlot.IsEmpty) return;

            var item = itemSlot.Item;
            /*var simpleItemDetailQuantity = new SimpleItemDetailQuantity
            {
                ItemDetail = item.ItemDetail as SimpleItemDetail,
                Quantity = item.Quantity
            };#1#
            _inventoryManager.AddItem(item);
            itemSlot.RemoveItem();
        }

        public void MoveItemFromInventoryToEmptySlot(SimpleItemDetail simpleItemDetail, int quantity = 1)
        {
            if (!HasEmptySlot()) return;
            if (!_inventoryManager.HasEnoughItem(simpleItemDetail, quantity)) return;

            var emptySlot = GetEmptySlot();
            if (emptySlot == null) return;

            _inventoryManager.RemoveItem(simpleItemDetail, quantity);

            var newItem = new SimpleItem(simpleItemDetail, quantity);
            _inventoryManager.AddItem(newItem);
        }*/
        protected override void OnSlotItemChanged(ItemSlotBase<SimpleItem> itemSlot, SimpleItem newItem) { }
        protected override void OnSlotItemRemoved(ItemSlotBase<SimpleItem> itemSlot, SimpleItem removedItem) { }
    }

    [Serializable]
    public class SimpleItemSlotData
    {
        public ItemType[] AllowedItemTypes;
    }
}
