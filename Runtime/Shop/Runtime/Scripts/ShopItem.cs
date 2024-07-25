using Boshphelm.Currencies;
using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopItem
    {
        public ShopItemDetails shopItemDetails;
        public bool bought;
        public int itemLevel;
        public bool equipped;
        public bool itemMaxLevel;
        public ShopItemType ShopItemType;

        public Price PriceToBuy => shopItemDetails.price;
        public Price PriceToNextUpgrade => shopItemDetails.ShopItemUpgrades[itemLevel].Price;

        public System.Action<ShopItem> OnBuyRequested = _ => { };
        public System.Action<ShopItem> OnUpgradeRequested = _ => { };
        public System.Action<ShopItem> OnEquipRequested = _ => { };

        public ShopItem(ShopItemDetails shopItemDetails, bool equipped, bool bought, int itemLevel = 0)
        {
            this.shopItemDetails = shopItemDetails;
            this.bought = bought;
            this.itemLevel = itemLevel;
            this.equipped = equipped; // TODO : Add Locked Status.
            UpdateMaxLevelAndType();
        }

        public void UpdateMaxLevelAndType()
        {
            itemMaxLevel = itemLevel >= shopItemDetails.itemDetails.MaxItemLevel;
            ShopItemType = itemMaxLevel ? ShopItemType.MaxLevel : ShopItemType.Available;
        }
    }

    public enum ShopItemType
    {
        Available,
        Locked,
        MaxLevel
    }
}