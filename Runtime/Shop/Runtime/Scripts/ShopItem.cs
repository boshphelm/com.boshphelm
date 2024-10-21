using System;
using Boshphelm.Currencies;
using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopItem
    {
        public IShopItemDetails Details { get; }
        public bool IsBought { get; private set; }
        public int ItemLevel { get; private set; }
        public bool IsEquipped { get; private set; }
        public bool IsMaxLevel => ItemLevel >= Details.MaxLevel;
        public ShopItemState State { get; private set; }

        public Price PriceToBuy => Details.BuyPrice;
        public Price PriceToNextUpgrade => Details.GetUpgradePrice(ItemLevel);

        public event Action<ShopItem> OnBuyRequested;
        public event Action<ShopItem> OnUpgradeRequested;
        public event Action<ShopItem> OnEquipRequested;

        public ShopItem(IShopItemDetails details, bool isEquipped, bool isBought, int itemLevel = 0)
        {
            Details = details;
            IsEquipped = isEquipped;
            IsBought = isBought;
            ItemLevel = itemLevel;
            UpdateState();
        }

        public void Buy()
        {
            IsBought = true;
            UpdateState();
            OnBuyRequested?.Invoke(this);
        }

        public void Upgrade()
        {
            if (!IsMaxLevel)
            {
                ItemLevel++;
                UpdateState();
                OnUpgradeRequested?.Invoke(this);
            }
        }

        public void Equip()
        {
            IsEquipped = true;
            UpdateState();
            OnEquipRequested?.Invoke(this);
        }

        public void Unequip()
        {
            IsEquipped = false;
            UpdateState();
        }

        private void UpdateState()
        {
            State = IsMaxLevel ? ShopItemState.MaxLevel :
                    !IsBought ? ShopItemState.Available :
                    ShopItemState.Upgradeable;
        }
    }

    public enum ShopItemState
    {
        Available,
        Upgradeable,
        MaxLevel
    }
}