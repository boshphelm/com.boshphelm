using Boshphelm.Inventories;
using Boshphelm.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Shops
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private ShopItemUpgradeView _shopItemUpgradeView;
        [SerializeField] private ShopItemUpgradeView _shopItemStarView;
        [SerializeField] private ShopItemStatView[] _statsViews;
        [SerializeField] private GameObject _equippedGO;

        private ShopItem _shopItem;

        public ShopItem ShopItem => _shopItem;

        public void RefreshView(ShopItem shopItem)
        {
            _shopItem = shopItem;
            ItemDetail itemDetail = shopItem.Details.ItemDetail;
            _titleText.text = itemDetail.DisplayName;
            _itemIcon.sprite = itemDetail.ShopUI.Icon;
            _itemIcon.rectTransform.sizeDelta = itemDetail.ShopUI.size;
            _itemIcon.rectTransform.rotation = itemDetail.ShopUI.rotation;

            SetItemUpgradeAndStarView(shopItem);
            SetItemStatView(shopItem);

            _equippedGO.SetActive(shopItem.IsEquipped);
        }

        private void SetItemUpgradeAndStarView(ShopItem shopItem)
        {
            ShopItemUpgradeDegree shopItemUpgradeDegree = new ShopItemUpgradeDegree(shopItem.ItemLevel, 5);
            _shopItemUpgradeView?.RefreshView(shopItemUpgradeDegree.upgradeLevel);
            _shopItemStarView?.RefreshView(shopItemUpgradeDegree.starLevel);
        }

        private void SetItemStatView(ShopItem shopItem)
        {
            if (_statsViews.Length == 0) return;
            for (int i = 0; i < _statsViews.Length; i++)
                _statsViews[i].RefreshView(shopItem.Details.ItemDetail.ItemStats[i], shopItem.ItemLevel);
        }

        // TRIGGER BY UI
        public void BuyShopItem()
        {
            _shopItem?.Buy();
        }

        public void UpgradeShopItem()
        {
            _shopItem?.Upgrade();
        }

        public void EquipShopItem()
        {
            _shopItem?.Equip();
        }
    }

    public struct ShopItemUpgradeDegree
    {
        public int upgradeLevel;
        public int starLevel;

        public ShopItemUpgradeDegree(int itemLevel, int itemLevelForPerStar)
        {
            starLevel = itemLevel == 0 ? 0 : itemLevel / itemLevelForPerStar;
            upgradeLevel = itemLevel == 0 ? 0 : itemLevel % itemLevelForPerStar;
        }
    }
}