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

        public ShopItem ShopItem { get; private set; }

        public void RefreshView(ShopItem shopItem)
        {
            ShopItem = shopItem;
            ItemDetail itemDetail = shopItem.shopItemDetails.itemDetails;
            _titleText.text = itemDetail.DisplayName;
            _itemIcon.sprite = itemDetail.ShopUI.Icon;
            _itemIcon.rectTransform.sizeDelta = itemDetail.ShopUI.size;
            _itemIcon.rectTransform.rotation = itemDetail.ShopUI.rotation;

            SetItemUpgradeAndStarView(shopItem);
            SetItemStatView(shopItem);

            _equippedGO.SetActive(shopItem.equipped);
        }

        private void SetItemUpgradeAndStarView(ShopItem shopItem)
        {
            ShopItemUpgradeDegree shopItemUpgradeDegree = new ShopItemUpgradeDegree(shopItem.itemLevel, 5);
            _shopItemUpgradeView?.RefreshView(shopItemUpgradeDegree.upgradeLevel);
            _shopItemStarView?.RefreshView(shopItemUpgradeDegree.starLevel);
        }

        private void SetItemStatView(ShopItem shopItem)
        {
            if(_statsViews.Length == 0) return;
            for(int i = 0; i < _statsViews.Length; i++)  
                _statsViews[i].RefreshView(shopItem.shopItemDetails.itemDetails.ItemStats[i], shopItem.itemLevel); 
        }
        
        // TRIGGER BY UI
        public void BuyShopItem()
        {
            ShopItem.OnBuyRequested.Invoke(ShopItem);
        }

        public void UpgradeShopItem()
        {
            ShopItem.OnUpgradeRequested.Invoke(ShopItem);
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