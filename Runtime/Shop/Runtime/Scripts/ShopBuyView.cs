using UnityEngine;
using Boshphelm.Currencies;
using Boshphelm.Wallets;
using Boshphelm.Utility;
using Boshphelm.Items;
using System;

namespace Boshphelm.Shops
{
    public class ShopBuyView : MonoBehaviour, IShopBuyView
    {
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private GameObject buyGO;
        [SerializeField] private PriceView buyPriceView;
        [SerializeField] private GameObject upgradeGO;
        [SerializeField] private PriceView upgradePriceView;
        [SerializeField] private GameObject equipGO;

        [SerializeField] private VoidEventChannel onSave;
        [SerializeField] private ItemReturnItemDetailEventChannel onItemRequestedByItemDetail;
        [SerializeField] private ItemDetailIntEventChannel onItemAdd;

        private ShopItem currentShopItem;

        public event Action<ShopItem> OnRefreshRequired;
        public event Action OnCurrentWeaponUpgraded;



        public void RefreshView(ShopItem shopItem)
        {
            currentShopItem = shopItem;
            SetBuyViewByShopItemState(shopItem);
        }

        public void HandleItemBuy()
        {
            if (currentShopItem == null || IsItemDetailsAlreadyInInventory(currentShopItem.Details.ItemDetail)) return;
            if (!TryToPayThePrice(currentShopItem.PriceToBuy)) return;

            CreateAnItemAndAddInventory(currentShopItem.Details.ItemDetail);
            currentShopItem.Buy();
            OnRefreshRequired?.Invoke(currentShopItem);
            onSave.RaiseEvent();
        }

        public void HandleItemUpgrade()
        {
            if (currentShopItem == null || !currentShopItem.IsBought || currentShopItem.IsMaxLevel) return;
            if (!TryToPayThePrice(currentShopItem.PriceToNextUpgrade)) return;

            currentShopItem.Upgrade();
            OnRefreshRequired?.Invoke(currentShopItem);
            OnCurrentWeaponUpgraded?.Invoke();
            onSave.RaiseEvent();
        }

        public void HandleItemEquip()
        {
            if (currentShopItem == null || !currentShopItem.IsBought) return;

            currentShopItem.Equip();
            OnRefreshRequired?.Invoke(currentShopItem);
        }

        private bool IsItemDetailsAlreadyInInventory(ItemDetail itemDetail)
        {
            return onItemRequestedByItemDetail.RaiseEvent(itemDetail) != null;
        }

        private bool TryToPayThePrice(Price price)
        {
            if (!playerWallet.HaveEnoughCurrency(price.CurrencyDetails, price.Amount)) return false;

            playerWallet.RemoveCurrency(price.CurrencyDetails, price.Amount);
            return true;
        }

        private void CreateAnItemAndAddInventory(ItemDetail itemDetail)
        {
            var newBoughtItem = itemDetail.Create(1);
            onItemAdd.RaiseEvent(itemDetail, newBoughtItem.Quantity);
        }

        private void SetBuyViewByShopItemState(ShopItem shopItem)
        {
            switch (shopItem.State)
            {
                case ShopItemState.Available:
                    SetAvailableView(shopItem);
                    break;
                case ShopItemState.Upgradeable:
                    SetUpgradeableView(shopItem);
                    break;
                case ShopItemState.MaxLevel:
                    SetMaxLevelView();
                    break;
            }
        }

        private void SetAvailableView(ShopItem shopItem)
        {
            buyGO.SetActive(true);
            upgradeGO.SetActive(false);
            equipGO.SetActive(false);
            buyPriceView.SetPrice(shopItem.PriceToBuy);
        }

        private void SetUpgradeableView(ShopItem shopItem)
        {
            buyGO.SetActive(false);
            upgradeGO.SetActive(true);
            equipGO.SetActive(!shopItem.IsEquipped);
            upgradePriceView.SetPrice(shopItem.PriceToNextUpgrade);
        }

        private void SetMaxLevelView()
        {
            buyGO.SetActive(false);
            upgradeGO.SetActive(false);
            equipGO.SetActive(false);
        }
    }
}