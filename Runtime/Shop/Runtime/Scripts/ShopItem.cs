using System;
using Boshphelm.Currencies;
using Boshphelm.Items;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopItem
    {
        public IShopItemDetails Details { get; }
        public ItemDetail ItemDetail => Details.ItemDetail;
        public int Quantity { get; private set; }
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

        public ShopItem(IShopItemDetails details, bool isEquipped, bool isBought, int itemLevel = 0, int quantity = 1)
        {
            Details = details;
            Quantity = quantity;
            IsEquipped = isEquipped;
            IsBought = isBought;
            ItemLevel = itemLevel;

            UpdateState();
        }
        public void CompletePurchase()
        {
            IsBought = true;
            UpdateState();
        }

        public void CompleteUpgrade()
        {
            if (!IsMaxLevel)
            {
                ItemLevel++;
                UpdateState();
            }
        }
        public void Equip()
        {
            IsEquipped = true;
            UpdateState();
            OnEquipRequested?.Invoke(this);

            Debug.Log($"Item Equipped {this}");
        }
        public void Unequip()
        {
            IsEquipped = false;
            UpdateState();
        }

        public bool CanBuy() => !IsBought;
        public bool CanEquip() => IsBought && !IsEquipped;
        public bool CanUpgrade() => IsBought && !IsMaxLevel;

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