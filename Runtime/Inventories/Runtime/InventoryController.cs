using System.Collections.Generic;
using Boshphelm.Items;
using Boshphelm.Utility;

namespace Boshphelm.Inventories
{
    public class InventoryController
    {
        private readonly InventoryModel _inventoryModel;
        // TODO: InventoryView ??

        public InventoryController(List<Item> items)
        {
            _inventoryModel = new InventoryModel(items);
        }

        public void AddItem(ItemDetail itemDetail, int quantity) => AddItem(itemDetail.Create(quantity));
        public void AddItem(Item item) => _inventoryModel.AddItem(item);
        public void RemoveItem(ItemDetail itemDetail, int quantity) => RemoveItem(itemDetail.Create(quantity));
        public bool RemoveItem(Item item) => _inventoryModel.RemoveItem(item);
        public Item GetByItemDetail(ItemDetail itemDetail) => _inventoryModel.GetItemByItemDetail(itemDetail);
        public Item GetItemByItemDetailId(SerializableGuid itemDetailId) => _inventoryModel.GetItemByItemDetailId(itemDetailId);
        public bool HasEnoughItem(ItemDetail itemDetail, int quantity) => HasEnoughItem(itemDetail.Create(quantity));
        public bool HasEnoughItem(Item item) => _inventoryModel.HasEnoughItem(item);
        public int GetItemCountByItem(Item item) => _inventoryModel.GetItemCountByItem(item);
        public int GetItemCountByItemDetail(ItemDetail itemDetail) => _inventoryModel.GetItemCountByItemDetail(itemDetail);

        public List<ItemSaveData> GenerateSaveData() => _inventoryModel.GenerateSaveData();
    }
}