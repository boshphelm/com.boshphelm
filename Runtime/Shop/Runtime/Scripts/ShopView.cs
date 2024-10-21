using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopView : MonoBehaviour, IShopView
    {
        [SerializeField] private ShopItemView[] shopItemViews;

        public void RefreshView(ShopItem[] shopItems)
        {
            for (int i = 0; i < shopItems.Length && i < shopItemViews.Length; i++)
            {
                shopItemViews[i].RefreshView(shopItems[i]);
            }
        }

        public void RefreshShopItemView(ShopItem shopItem)
        {
            foreach (var itemView in shopItemViews)
            {
                if (itemView.ShopItem.Details.ItemDetail.Id == shopItem.Details.ItemDetail.Id)
                {
                    itemView.RefreshView(shopItem);
                    break;
                }
            }
        }
    }
}