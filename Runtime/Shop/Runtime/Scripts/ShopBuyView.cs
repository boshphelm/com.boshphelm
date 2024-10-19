using Boshphelm.Currencies;
using Boshphelm.Inventories;
using Boshphelm.Items;
using Boshphelm.Utility;
using Boshphelm.Wallets;
using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopBuyView : MonoBehaviour
    {
        [SerializeField] private Wallet _playerWallet;

        [Header("Payment & Price")]
        [SerializeField] private GameObject _buyGO;
        [SerializeField] private PriceView _buyPriceView;
        [SerializeField] private GameObject _upgradeGO;
        [SerializeField] private PriceView _upgradePriceView;
        [SerializeField] private GameObject _equipGO;

        private ShopItem _currentShopItem;

        public System.Action<ShopItem> OnRefreshRequired = _ => { };
        public System.Action OnCurrentWeaponUpgraded;

        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannel _onSave;
        [SerializeField] private ItemReturnItemDetailEventChannel _onItemRequestedByItemDetail;
        [SerializeField] private ItemDetailIntEventChannel _onItemAdd;

        public void RefreshView(ShopItem shopItem)
        {
            //Debug.Log("REFRESHING VIEW FOR SHOP ITEM : " + shopItem.itemLevel, shopItem.shopItemDetails);
            _currentShopItem = shopItem;
            SetBuyViewByShopItemType(shopItem);
        }

        // TRIGGER BY UI -> ShopCanvas - ShopBuyButtons - BuyButton
        public void HandleItemBuy()
        {
            if (_currentShopItem == null) return;

            if (IsItemDetailsAlreadyInInventory(_currentShopItem.shopItemDetails.itemDetails)) return;
            if (!TryToPayThePrice(_currentShopItem.PriceToBuy)) return;

            CreateAnItemAndAddInventory(_currentShopItem.shopItemDetails.itemDetails);

            SetPlayerMainItem(_currentShopItem);

            TriggerRefreshRequire(_currentShopItem);

            _onSave.RaiseEvent();
        }

        private bool IsItemDetailsAlreadyInInventory(ItemDetail itemDetail)
        {
            var item = _onItemRequestedByItemDetail.RaiseEvent(itemDetail);
            bool isItemAlreadyInInventory = item != null;

            return isItemAlreadyInInventory;
        }

        private bool TryToPayThePrice(Price price)
        {
            if (!IsPricePayable(price)) return false;

            _playerWallet.RemoveCurrency(price.CurrencyDetails, price.Amount);
            return true;
        }

        private void CreateAnItemAndAddInventory(ItemDetail itemDetail)
        {
            var newBoughtItem = itemDetail.Create(1);
            _onItemAdd.RaiseEvent(itemDetail, newBoughtItem.Quantity);
        }

        private void SetPlayerMainItem(ShopItem shopItem)
        {
            /*
             _playerItemLevel.UpgradeItemLevel(shopItem.shopItemDetails.itemDetails);
             _playerMainItem.SetMainItem(shopItem.shopItemDetails.itemDetails);
             */
        }

        private bool IsPricePayable(Price price) => _playerWallet.HaveEnoughCurrency(price.CurrencyDetails, price.Amount);

        // TRIGGER BY UI -> ShopCanvas - ShopBuyUpgradeButtons - UpgradeButton
        public void HandleItemUpgrade()
        {
            if (_currentShopItem == null) return;

            if (!IsItemDetailsAlreadyInInventory(_currentShopItem.shopItemDetails.itemDetails)) return;
            if (!TryToPayThePrice(_currentShopItem.PriceToNextUpgrade)) return;

            //_playerItemLevel.UpgradeItemLevel(_currentShopItem.shopItemDetails.itemDetails);
            TriggerRefreshRequire(_currentShopItem);

            OnCurrentWeaponUpgraded?.Invoke();

            _onSave.RaiseEvent();
        }

        // TRIGGER BY UI -> ShopCanvas - ShopEquipButton - EquipButton
        public void HandleItemEquip()
        {
            if (_currentShopItem == null) return;

            _currentShopItem.OnEquipRequested.Invoke(_currentShopItem);
            TriggerRefreshRequire(_currentShopItem);
        }

        private void TriggerRefreshRequire(ShopItem shopItem)
        {
            OnRefreshRequired.Invoke(shopItem);
        }

        private void SetBuyViewByShopItemType(ShopItem shopItem)
        {
            if (shopItem.ShopItemType == ShopItemType.Locked || shopItem.ShopItemType == ShopItemType.MaxLevel)
            {
                DeactivateAllPriceViews();
            }
            else
            {
                SetAvailableShopItem(shopItem);
            }
        }

        private void SetAvailableShopItem(ShopItem availableShopItem)
        {
            // Debug.Log("AVAILABLE SHOP ITEM : " + availableShopItem.shopItemDetails.itemDetails.displayName + ", BOUGHT : " + availableShopItem.bought, availableShopItem.shopItemDetails);
            if (availableShopItem.bought)
            {
                if (availableShopItem.equipped)
                {
                    ActivateOnlyUpgradePriceView();
                    _upgradePriceView.SetPrice(availableShopItem.PriceToNextUpgrade);
                }
                else
                {
                    ActivateOnlyEquipView();
                }
            }
            else
            {
                ActivateOnlyBuyPriceView();
                _buyPriceView.SetPrice(availableShopItem.PriceToBuy);
            }
        }

        private void ActivateOnlyEquipView()
        {
            _buyGO.SetActive(false);
            _upgradeGO.SetActive(false);
            _equipGO.SetActive(true);
        }

        private void ActivateOnlyUpgradePriceView()
        {
            _buyGO.SetActive(false);
            _upgradeGO.SetActive(true);
            _equipGO.SetActive(false);
        }


        private void ActivateOnlyBuyPriceView()
        {
            _buyGO.SetActive(true);
            _upgradeGO.SetActive(false);
            _equipGO.SetActive(false);
        }

        private void DeactivateAllPriceViews()
        {
            _buyGO.SetActive(false);
            _upgradeGO.SetActive(false);
            _equipGO.SetActive(false);
        }
    }
}
