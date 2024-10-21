using System;
using System.Collections.Generic;

namespace Boshphelm.Shops
{
    public class ShopModel : IShopModel
    {
        private List<ShopItem> shopItems;

        public event Action<ShopItem[]> OnModelChanged;

        public ShopItem[] Items => shopItems.ToArray();

        public ShopModel(IEnumerable<ShopItem> items)
        {
            shopItems = new List<ShopItem>(items);
        }

        public ShopItem GetItem(int index) => shopItems[index];

        public void BuyItem(int index)
        {
            shopItems[index].Buy();
            OnModelChanged?.Invoke(Items);
        }

        public void UpgradeItem(int index)
        {
            shopItems[index].Upgrade();
            OnModelChanged?.Invoke(Items);
        }
    }
}