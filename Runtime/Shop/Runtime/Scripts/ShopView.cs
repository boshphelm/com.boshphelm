using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private ShopItemView[] shopItemViews;

        public void RefreshView(ShopItem[] shopItems)
        {
            ShopItemCountCheck(shopItems.Length);

            for (int i = 0; i < shopItems.Length; i++)
            {
                shopItemViews[i].RefreshView(shopItems[i]);
            }
        }

        public void RefreshShopItemView(ShopItem shopItem)
        {
            for (int i = 0; i < shopItemViews.Length; i++)
            {
                if (shopItemViews[i].ShopItem.shopItemDetails.itemDetails.Id != shopItem.shopItemDetails.itemDetails.Id) continue;

                shopItemViews[i].RefreshView(shopItem);
                return;
            }
        }

        private void ShopItemCountCheck(int shopItemCount)
        {
            if (shopItemCount < shopItemViews.Length)
            {
                Debug.LogError("SHOP ITEM COUNT < SHOP ITEM VIEW COUNT");
            }
            else if (shopItemCount > shopItemViews.Length)
            {
                Debug.LogError("SHOP ITEM COUNT > SHOP ITEM VIEW COUNT");
            }
        }
    }
}