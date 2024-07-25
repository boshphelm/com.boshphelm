using System;
using Boshphelm.Inventories;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopModel
    {
        public ObservableArray<ShopItem> ShopItems { get; set; }
 
        private readonly int _capacity;

        public event Action<ShopItem[]> OnModelChanged
        {
            add => ShopItems.AnyValueChanged += value;
            remove => ShopItems.AnyValueChanged -= value;
        }

        public ShopModel(ShopItem[] shopItems)
        {
            ShopItems = new ObservableArray<ShopItem>(shopItems.Length);
            foreach (var shopItem in shopItems)
            {
                var result = ShopItems.TryAdd(shopItem);
            }
        } 

        public ShopItem Get(int index) => ShopItems[index];

        public void Buy(int index) => ShopItems[index].bought = true;
        public void Upgrade(int index) => ShopItems[index].itemLevel++;
    }
}