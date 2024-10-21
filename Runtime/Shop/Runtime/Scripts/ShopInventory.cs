using System.Collections.Generic;
using Boshphelm.Utility;

namespace Boshphelm.Shops
{
    public class ShopInventory
    {
        private Dictionary<SerializableGuid, ShopItem> _shopItems = new Dictionary<SerializableGuid, ShopItem>();

        public void AddItem(ShopItem item)
        {
            if (!_shopItems.ContainsKey(item.ItemDetail.Id))
            {
                _shopItems[item.ItemDetail.Id] = item;
            }
        }

        public ShopItem GetItem(SerializableGuid itemId)
        {
            return _shopItems.TryGetValue(itemId, out var item) ? item : null;
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            return _shopItems.Values;
        }
    }
}