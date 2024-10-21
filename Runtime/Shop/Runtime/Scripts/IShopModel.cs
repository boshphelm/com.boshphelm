using System;

namespace Boshphelm.Shops
{
    public interface IShopModel
    {
        event Action<ShopItem[]> OnModelChanged;
        ShopItem[] Items { get; }
        ShopItem GetItem(int index);
        void BuyItem(int index);
        void UpgradeItem(int index);
    }
}