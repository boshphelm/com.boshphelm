using System;

namespace Boshphelm.Shops
{
    public interface IShopBuyView
    {
        void RefreshView(ShopItem shopItem);
        event Action<ShopItem> OnRefreshRequired;
    }
}
