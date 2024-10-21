using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Shops
{
    public class ShopUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _shopItemPrefab;
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;
        [SerializeField] private TextMeshProUGUI _priceText;

        private ShopManager _shopManager;
        private List<ShopItemUI> _shopItemUIs = new List<ShopItemUI>();
        private ShopItem _selectedItem;

        public void Initialize(ShopManager manager)
        {
            _shopManager = manager;
            _buyButton.onClick.AddListener(OnBuyClicked);
            _upgradeButton.onClick.AddListener(OnUpgradeClicked);
            _equipButton.onClick.AddListener(OnEquipClicked);
        }

        public void UpdateShopUI()
        {
            ClearItemContainer();
            foreach (var item in _shopManager.GetAllShopItems())
            {
                CreateItemUI(item);
            }
            UpdateSelectedItemUI(null);
        }

        private void ClearItemContainer()
        {
            foreach (var itemUI in _shopItemUIs)
            {
                Destroy(itemUI.gameObject);
            }
            _shopItemUIs.Clear();
        }

        private void CreateItemUI(ShopItem item)
        {
            var itemGO = Instantiate(_shopItemPrefab, _itemContainer);
            var itemUI = itemGO.GetComponent<ShopItemUI>();
            itemUI.SetupItem(item, OnItemSelected);
            _shopItemUIs.Add(itemUI);
        }

        private void OnItemSelected(ShopItem item)
        {
            _selectedItem = item;
            UpdateSelectedItemUI(item);
        }

        private void UpdateSelectedItemUI(ShopItem item)
        {
            if (item == null)
            {
                _itemNameText.text = "";
                _itemDescriptionText.text = "";
                _priceText.text = "";
                _buyButton.gameObject.SetActive(false);
                _upgradeButton.gameObject.SetActive(false);
                _equipButton.gameObject.SetActive(false);
                return;
            }

            _itemNameText.text = item.ItemDetail.DisplayName;
            _itemDescriptionText.text = item.ItemDetail.Description;

            _buyButton.gameObject.SetActive(item.CanBuy());
            _upgradeButton.gameObject.SetActive(item.CanUpgrade());
            _equipButton.gameObject.SetActive(item.IsBought && !item.IsEquipped);

            if (item.CanBuy())
                _priceText.text = $"Buy: {item.PriceToBuy.Amount}";
            else if (item.CanUpgrade())
                _priceText.text = $"Upgrade: {item.PriceToNextUpgrade.Amount}";
            else
                _priceText.text = "Max Level";
        }

        private void OnBuyClicked()
        {
            if (_selectedItem != null && _shopManager.TryBuyItem(_selectedItem))
            {
                UpdateSelectedItemUI(_selectedItem);
            }
            else
            {
                Debug.Log("Not enough money to buy the item!");
            }
        }

        private void OnUpgradeClicked()
        {
            if (_selectedItem != null && _shopManager.TryUpgradeItem(_selectedItem))
            {
                UpdateSelectedItemUI(_selectedItem);
            }
            else
            {
                Debug.Log("Not enough money to upgrade the item!");
            }
        }

        private void OnEquipClicked()
        {
            if (_selectedItem != null && _shopManager.TryEquipItem(_selectedItem))
            {
                UpdateSelectedItemUI(_selectedItem);
            }
            else
            {
                Debug.Log("Cannot equip the item!");
            }
        }
    }
}