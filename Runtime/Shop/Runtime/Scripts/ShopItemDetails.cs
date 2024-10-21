using UnityEngine;
using Boshphelm.Currencies;
using Boshphelm.Items;

namespace Boshphelm.Shops
{
    [CreateAssetMenu(fileName = "New Shop Item Detail", menuName = "Boshphelm/Shop/ShopItemDetail")]
    public class ShopItemDetails : ScriptableObject, IShopItemDetails
    {
        [SerializeField] private ItemDetail itemDetails;
        [SerializeField] private Price buyPrice;
        [SerializeField] private ShopItemUpgrade[] upgrades;

        public ItemDetail ItemDetail => itemDetails;
        public Price BuyPrice => buyPrice;
        public int MaxLevel => upgrades.Length;

        public Price GetUpgradePrice(int level)
        {
            return level < upgrades.Length ? upgrades[level].Price : null;
        }
    }

    [System.Serializable]
    public class ShopItemUpgrade
    {
        public Price Price;
    }
}
