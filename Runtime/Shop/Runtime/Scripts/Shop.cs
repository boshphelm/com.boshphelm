using System.Collections.Generic;
using Boshphelm.Inventories;
using Boshphelm.Items;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Shops
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ShopView _view;
        [SerializeField] private List<ShopItemDetails> _shopItemDetails;
        [SerializeField] private ScrollSnap _scrollSnap;
        [SerializeField] private ShopBuyView _shopBuyView;
        [SerializeField] private GameObject _shopGO;

        private ShopController _controller;

        public void Init()
        {
            var shopItems = GenerateShopItems();

            _controller = new ShopController.Builder(_view, _scrollSnap, _shopBuyView)
                .WithStartingItems(shopItems.ToArray())
                .Build();

            _shopBuyView.OnRefreshRequired += OnRefreshRequiredForShopItem;
        }


        private List<ShopItem> GenerateShopItems()
        {
            var shopItems = new List<ShopItem>();
            foreach (var shopItemDetail in _shopItemDetails)
            {
                if (shopItemDetail == null) continue;

                var generatedShopItem = GenerateShopItem(shopItemDetail);
                generatedShopItem.OnEquipRequested += OnShopItemEquipRequested;
                shopItems.Add(generatedShopItem);
            }

            return shopItems;
        }

        private void OnShopItemEquipRequested(ShopItem shopItem)
        {
            //_playerMainItem.SetMainItem(shopItem.shopItemDetails.itemDetails);
        }

        private ShopItem GenerateShopItem(ShopItemDetails shopItemDetail)
        {
            bool bought = _inventory.GetItemByItemDetail(shopItemDetail.itemDetails) != null;
            int itemLevel = 0;
            /*  int itemLevel =bought
                ? _playerItemLevel.GetItemLevel(shopItemDetail.itemDetails) //TODO
                : 0;
            */
            bool equipped = false;
            /* _playerMainItem.IsItemDetailIdMain(shopItemDetail.itemDetails.Id); */ //TODO

            //Debug.Log("ITEM : " + shopItemDetail.itemDetails + ", LEVEL : " + itemLevel, shopItemDetail.itemDetails);
            return shopItemDetail.Create(equipped, bought, itemLevel);
        }

        public void OnRefreshRequiredForShopItem(ShopItem shopItem)
        {
            UpdateAllShopItems(_controller.Items);

            _controller.RefreshView();
        }

        private void UpdateAllShopItems(ShopItem[] shopItems)
        {
            foreach (var shopItem in shopItems)
            {
                UpdateShopItem(shopItem);
            }
        }

        private void UpdateShopItem(ShopItem shopItem)
        {
            shopItem.bought = _inventory.GetItemByItemDetail(shopItem.shopItemDetails.itemDetails) != null;
            shopItem.itemLevel = 0;
            /* shopItem.itemLevel = shopItem.bought
                ? _playerItemLevel.GetItemLevel(shopItem.shopItemDetails.itemDetails)
                : 0; */
            //shopItem.equipped = _playerMainItem.IsItemDetailIdMain(shopItem.shopItemDetails.itemDetails.Id);
            //Debug.Log("ITEM : " + shopItem.shopItemDetails.itemDetails + ", LEVEL : " + shopItem.itemLevel, shopItem.shopItemDetails.itemDetails);
            shopItem.UpdateMaxLevelAndType();
        }

        public void OpenShop()
        {
            _shopGO.SetActive(true);
            _controller.RefreshView();
        }

        public void CloseShop()
        {
            _shopGO.SetActive(false);
        }
    }
}
