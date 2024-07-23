using System.Collections.Generic;
using System.Linq;
using Boshphelm.Items;
using Boshphelm.Utility;

namespace Boshphelm.Inventories
{
    public class InventoryModel
    {
        private readonly List<Item> _items;

        public InventoryModel(IEnumerable<Item> items)
        {
            _items = new List<Item>(items);
        }

        public Item GetItemById(SerializableGuid Id) => FindItemById(Id);
        private Item FindItemById(SerializableGuid Id) => _items.FirstOrDefault(item => item.Id == Id);

        public void AddItem(Item item)
        {
            Item itemWithSameItemDetailInInventory = FindItemByItemDetailId(item.ItemDetailId);
            if (itemWithSameItemDetailInInventory != null && itemWithSameItemDetailInInventory.ItemDetail.Stackable)
            {
                itemWithSameItemDetailInInventory.Quantity += item.Quantity;
            }
            else
            {
                _items.Add(item);
            }
        }

        public Item GetItemByItemDetail(ItemDetail itemDetail) => FindItemByItemDetailId(itemDetail.Id);
        public Item GetItemByItemDetailId(SerializableGuid itemDetailSerializableGuid) => FindItemByItemDetailId(itemDetailSerializableGuid);
        private Item FindItemByItemDetailId(SerializableGuid itemDetailId) => _items.FirstOrDefault(item => item.ItemDetailId == itemDetailId);

        public bool RemoveItem(Item item)
        {
            if (!HasEnoughItem(item)) return false;

            RemoveAndRestockItemsByItem(item);

            return true;
        }

        public bool HasEnoughItem(Item item) => GetItemCountByItemDetailId(item.ItemDetailId) >= item.Quantity;

        private void RemoveAndRestockItemsByItem(Item item)
        {
            var itemsWithSameItemDetailInInventory = FindItemsByItemDetailId(item.ItemDetailId);

            for (int i = itemsWithSameItemDetailInInventory.Count - 1; i >= 0; i--)
            {
                if (item.Quantity == 0) break;

                Item itemWithSameItemDetailInInventory = itemsWithSameItemDetailInInventory[i];
                if (itemWithSameItemDetailInInventory.ItemDetail.Stackable)
                {
                    if (item.Quantity >= itemWithSameItemDetailInInventory.Quantity)
                    {
                        item.Quantity -= itemWithSameItemDetailInInventory.Quantity;
                        _items.Remove(itemWithSameItemDetailInInventory);
                    }
                    else
                    {
                        itemWithSameItemDetailInInventory.Quantity -= item.Quantity;
                        item.Quantity = 0;
                    }
                }
                else
                {
                    item.Quantity -= 1;
                    _items.Remove(itemWithSameItemDetailInInventory);
                }
            }
        }

        public int GetItemCountByItem(Item item) => GetItemCountByItemDetail(item.ItemDetail);
        public int GetItemCountByItemDetail(ItemDetail itemDetail) => GetItemCountByItemDetailId(itemDetail.Id);
        private int GetItemCountByItemDetailId(SerializableGuid itemDetailId) => FindItemsByItemDetailId(itemDetailId).Sum(item => item.Quantity);
        private List<Item> FindItemsByItemDetailId(SerializableGuid itemDetailId) => _items.FindAll(item => item.ItemDetailId == itemDetailId);

        public List<ItemSaveData> GenerateSaveData()
        {
            var itemSaveDatas = new List<ItemSaveData>();
            foreach (Item item in _items)
            {
                ItemSaveData itemSaveData = new ItemSaveData
                {
                    Quantity = item.Quantity,
                    ItemDetailIdHex = item.ItemDetailId.ToHexString()
                };
            }

            return itemSaveDatas;
        }
    }
}