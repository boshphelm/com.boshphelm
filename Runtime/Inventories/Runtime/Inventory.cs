using System;
using System.Collections;
using System.Collections.Generic;
using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.Inventories
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemDetailQuantity[] _startingItems;

        private InventoryController _inventoryController;

        private void Awake()
        {
            _inventoryController = new InventoryController(GenerateStartingItems());
        }

        private List<Item> GenerateStartingItems()
        {
            var startingItems = new List<Item>();

            foreach (ItemDetailQuantity startingItem in _startingItems)
            {
                Item item = startingItem.ItemDetail.Create(startingItem.Quantity);
                startingItems.Add(item);
            }

            return startingItems;
        }
    }

    [Serializable]
    public class ItemDetailQuantity
    {
        public ItemDetail ItemDetail;
        public int Quantity;
    }
}