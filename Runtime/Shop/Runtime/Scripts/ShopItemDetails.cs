using Boshphelm.Currencies;
using Boshphelm.Inventories;
using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.Shops
{
    [CreateAssetMenu(fileName = "New Shop Item Detail", menuName = "Boshphelm/Shop/ShopItemDetail")]
    public class ShopItemDetails : ScriptableObject
    {
        public ItemDetail itemDetails;
        public Price price;
        public ShopItemUpgrade[] ShopItemUpgrades;

        public ShopItem Create(bool equipped, bool bought, int itemLevel = 0) => new ShopItem(this, equipped, bought, itemLevel);
    }

    [System.Serializable]
    public class ShopItemUpgrade
    {
        public Price Price;
    }
}