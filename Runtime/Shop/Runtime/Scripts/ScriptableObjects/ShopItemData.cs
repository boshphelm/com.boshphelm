using Boshphelm.Currencies;
using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.Shop
{
    [CreateAssetMenu(fileName = "ShopItemData", menuName = "Boshphelm/Shop/ShopItemData", order = 0)]
    public class ShopItemData : ScriptableObject 
    {
        public ItemDetail itemDetail;
        public Price price;
        public ItemUpgradeData shopItemUpgrade;
    }

    public class ItemUpgradeData
    {
        public Price price;
    }
}