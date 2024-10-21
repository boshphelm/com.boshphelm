using System.Collections.Generic;
using UnityEngine;
using Boshphelm.Items;
using Boshphelm.Inventories;
using Boshphelm.Wallets;

namespace Boshphelm.Shops
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Wallet _playerWallet;
        [SerializeField] private ShopUIController _uiController;
        [SerializeField] private List<ShopItemDetails> _availableItems;

        private ShopInventory _shopInventory;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _shopInventory = new ShopInventory();
            CreateInitialShopItems();
            _uiController.Initialize(this);
            UpdateUI();
        }
        public bool TryBuyItem(ShopItem item)
        {
            Debug.Log("0");
            Debug.Log(item.CanBuy());
            if (item.CanBuy() && _playerWallet.HaveEnoughCurrency(item.PriceToBuy.CurrencyDetails, item.PriceToBuy.Amount))
            {
                Debug.Log("1");
                _playerWallet.RemoveCurrency(item.PriceToBuy.CurrencyDetails, item.PriceToBuy.Amount);
                _playerInventory.AddItem(item.ItemDetail, 1);
                item.CompletePurchase();
                return true;
            }
            Debug.Log("2");
            return false;
        }

        public bool TryUpgradeItem(ShopItem item)
        {
            if (item.CanUpgrade() && _playerWallet.HaveEnoughCurrency(item.PriceToNextUpgrade.CurrencyDetails, item.PriceToNextUpgrade.Amount))
            {
                _playerWallet.RemoveCurrency(item.PriceToNextUpgrade.CurrencyDetails, item.PriceToNextUpgrade.Amount);
                item.CompleteUpgrade();
                var inventoryItem = _playerInventory.GetItemByItemDetail(item.ItemDetail);
                if (inventoryItem != null)
                {
                    inventoryItem.ItemLevel = item.ItemLevel;
                }
                return true;
            }
            return false;
        }

        public bool TryEquipItem(ShopItem item)
        {
            if (item.CanEquip())
            {
                item.Equip();
                return true;
            }
            return false;
        }
        private void CreateInitialShopItems()
        {
            foreach (ShopItemDetails itemDetails in _availableItems)
            {
                Item existingItem = _playerInventory.GetItemByItemDetail(itemDetails.ItemDetail);
                bool isBought = existingItem != null;
                int itemLevel = isBought ? existingItem.ItemLevel : 0;
                bool isEquipped = false;

                var shopItem = new ShopItem(itemDetails, isEquipped, isBought, itemLevel);
                shopItem.OnBuyRequested += HandleBuyRequest;
                shopItem.OnUpgradeRequested += HandleUpgradeRequest;
                shopItem.OnEquipRequested += HandleEquipRequest;
                _shopInventory.AddItem(shopItem);
            }
        }

        private void HandleBuyRequest(ShopItem shopItem)
        {
            Debug.Log("1");
            if (_playerWallet.HaveEnoughCurrency(shopItem.PriceToBuy.CurrencyDetails, shopItem.PriceToBuy.Amount))
            {
                Debug.Log("2");
                _playerWallet.RemoveCurrency(shopItem.PriceToBuy.CurrencyDetails, shopItem.PriceToBuy.Amount);
                _playerInventory.AddItem(shopItem.ItemDetail, 1);
                UpdateUI();
            }
        }

        private void HandleUpgradeRequest(ShopItem shopItem)
        {
            if (_playerWallet.HaveEnoughCurrency(shopItem.PriceToNextUpgrade.CurrencyDetails, shopItem.PriceToNextUpgrade.Amount))
            {
                _playerWallet.RemoveCurrency(shopItem.PriceToNextUpgrade.CurrencyDetails, shopItem.PriceToNextUpgrade.Amount);

                var inventoryItem = _playerInventory.GetItemByItemDetail(shopItem.ItemDetail);
                if (inventoryItem != null)
                {
                    inventoryItem.ItemLevel = shopItem.ItemLevel;
                }
                UpdateUI();
            }
        }

        private void HandleEquipRequest(ShopItem shopItem)
        {
            UpdateUI();
        }

        public IEnumerable<ShopItem> GetAllShopItems()
        {
            return _shopInventory.GetAllItems();
        }

        private void UpdateUI()
        {
            _uiController.UpdateShopUI();
        }
    }
}