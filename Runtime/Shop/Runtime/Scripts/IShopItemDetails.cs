using Boshphelm.Currencies;
using Boshphelm.Items;

public interface IShopItemDetails
{
    ItemDetail ItemDetail { get; }
    Price BuyPrice { get; }
    int MaxLevel { get; }
    Price GetUpgradePrice(int level);
}