using UnityEngine;
using System.Collections.Generic;
using Boshphelm.Utility;
using Boshphelm.Items;

namespace Boshphelm.Shops
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopView view;
        [SerializeField] private List<ShopItemDetails> shopItemDetails;
        [SerializeField] private ScrollSnap scrollSnap;
        [SerializeField] private ShopBuyView shopBuyView;
        [SerializeField] private GameObject shopGO;

        [SerializeField] private ItemReturnItemDetailEventChannel onItemRequestedByItemDetail;

        private IShopController controller;

        public void Init()
        {
            var shopItems = GenerateShopItems();
            var model = new ShopModel(shopItems);
            controller = new ShopController(model, view, scrollSnap, shopBuyView);

            shopBuyView.OnRefreshRequired += OnRefreshRequiredForShopItem;
        }

        private List<ShopItem> GenerateShopItems()
        {
            var shopItems = new List<ShopItem>();
            foreach (var itemDetail in shopItemDetails)
            {
                if (itemDetail == null) continue;

                var shopItem = GenerateShopItem(itemDetail);
                shopItems.Add(shopItem);
            }
            return shopItems;
        }

        private ShopItem GenerateShopItem(ShopItemDetails itemDetail)
        {
            bool bought = onItemRequestedByItemDetail.RaiseEvent(itemDetail.ItemDetail) != null;
            int itemLevel = 0; // TODO: Implement item level retrieval
            bool equipped = false; // TODO: Implement equipped status retrieval

            return new ShopItem(itemDetail, equipped, bought, itemLevel);
        }

        private void OnRefreshRequiredForShopItem(ShopItem shopItem)
        {
            controller.RefreshShopItemView(shopItem);
        }

        public void OpenShop()
        {
            shopGO.SetActive(true);
            controller.RefreshView();
        }

        public void CloseShop()
        {
            shopGO.SetActive(false);
        }
    }
}