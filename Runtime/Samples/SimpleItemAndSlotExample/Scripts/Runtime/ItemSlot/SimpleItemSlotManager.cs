using System;
using System.Collections.Generic;
using Boshphelm.Items;
using Boshphelm.ItemSlot;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemSlotManager : ItemSlotManagerBase<SimpleItem, SimpleItemSlot>
    {
        [SerializeField] private PlayerSimpleInventoryManager _inventoryManager;
        [SerializeField] private SimpleItemSlotData[] _itemSlotDatas;

        [Title("UIView Properties")]
        [SerializeField] private Transform _uiViewSlotParent;
        [SerializeField] private GameObject _itemSlotUIViewPrefab;

        private SimpleItemSlotUIViewManager _simpleItemSlotUIViewManager;

        public void Initialize()
        {
            var newItemSlots = new List<SimpleItemSlot>();
            for (int i = 0; i < _itemSlotDatas.Length; i++)
            {
                var itemSlot = new SimpleItemSlot(_itemSlotDatas[i].AllowedItemTypes);
                newItemSlots.Add(itemSlot);
            }

            GenerateSlots(newItemSlots);

            _simpleItemSlotUIViewManager = new SimpleItemSlotUIViewManager(_uiViewSlotParent, _itemSlotUIViewPrefab);
            _simpleItemSlotUIViewManager.Initialize(itemSlots);
        }

        public void MoveItemInsideSlotToInventory(SimpleItemSlot itemSlot)
        {
            if (itemSlot.IsEmpty) return;

            var item = itemSlot.Item;
            /*var simpleItemDetailQuantity = new SimpleItemDetailQuantity
            {
                ItemDetail = item.ItemDetail as SimpleItemDetail,
                Quantity = item.Quantity
            };*/
            _inventoryManager.AddItem(item);
            itemSlot.RemoveItem();
        }

        public void MoveItemFromInventoryToEmptySlot(SimpleItemDetail simpleItemDetail, int quantity = 1)
        {
            if (!HasEmptySlot()) return;
            if (!_inventoryManager.HasEnoughItem(simpleItemDetail, quantity)) return;

            var emptySlot = GetEmptySlot();
            if (emptySlot == null) return;

            _inventoryManager.RemoveItem(simpleItemDetail, quantity);

            var newItem = new SimpleItem(simpleItemDetail, quantity);
            _inventoryManager.AddItem(newItem);
        }
    }

    [Serializable]
    public class SimpleItemSlotData
    {
        public ItemType[] AllowedItemTypes;
    }
}
