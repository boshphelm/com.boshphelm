namespace Boshphelm.Shops
{
    public interface IShopView
    {
        void RefreshView(ShopItem[] shopItems);
        void RefreshShopItemView(ShopItem shopItem);
    }
}