using System.Collections.Generic;
using Boshphelm.Items;

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

        public void AddItem(Item item) => _inventoryModel.AddItem(item);
        public bool RemoveItem(Item item) => _inventoryModel.RemoveItem(item);
        public bool HasEnoughItem(Item item) => _inventoryModel.HasEnoughItem(item);
        public int GetItemCountByItem(Item item) => _inventoryModel.GetItemCountByItem(item);
        public int GetItemCountByItemDetail(ItemDetail itemDetail) => _inventoryModel.GetItemCountByItemDetail(itemDetail);
    }
}