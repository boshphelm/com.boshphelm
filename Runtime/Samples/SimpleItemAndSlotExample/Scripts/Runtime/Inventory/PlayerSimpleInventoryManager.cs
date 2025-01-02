using System;
using System.Collections.Generic;
using Boshphelm.Inventories;
using Boshphelm.Items;
using Boshphelm.Save;
using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class PlayerSimpleInventoryManager : InventoryManagerBase<SimpleItem, SimpleItemDetail, SimpleItemDetailQuantity>, ISaveable
    {
        [SerializeField] private List<SimpleItemDetailQuantity> _initialItems;

        [Title("Item Slot UI View Properties")]
        [SerializeField] private Transform _uiViewSlotParent;
        [SerializeField] private GameObject _itemSlotUIViewPrefab;

        [Title("Item Slot Manager Properties")]
        [SerializeField] private int _initialSlotCapacity;
        [SerializeField] private int _maxPoolCapacity = 100;

        private SimpleInventory _inventory;
        protected override Inventory<SimpleItem, SimpleItemDetail, SimpleItemDetailQuantity> Inventory => _inventory;

        private SimpleItemSlotManager _itemSlotManager;
        private SimpleItemSlotUIViewManager _itemSlotUIViewManager;

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize()
        {
            _inventory = new SimpleInventory();
            _itemSlotManager = new SimpleItemSlotManager(_initialSlotCapacity, _maxPoolCapacity);
            _itemSlotUIViewManager = new SimpleItemSlotUIViewManager(_uiViewSlotParent, _itemSlotUIViewPrefab);

            Inventory.OnItemAdd += OnItemAddToInventory;
            Inventory.OnItemRemove += OnItemRemoveFromInventory;

            Inventory.Initialize(_initialItems);

            _initialItems.Clear();
        }

        private void OnItemAddToInventory(SimpleItem item)
        {
            // TODO: Create Slot
            var slot = _itemSlotManager.CreateSlot();
            // TODO: Put Item In Slot.
            slot.SetItem(item);
            // TODO: Create Slot UI View And Bind with slot.
            _itemSlotUIViewManager.CreateSlotUI(slot);
        }

        private void OnItemRemoveFromInventory(SimpleItem item)
        {
            // TODO: Find Item Slot.
            var slot = _itemSlotManager.GetSlotByItem(item);
            // TODO: Remove Item From Slot.
            var removedItem = slot.RemoveItem();
            // TODO: Also Return Slot UI View To Pool
            _itemSlotUIViewManager.RemoveSlotUIBySlot(slot);
            // TODO: Return Slot To Pool
            _itemSlotManager.RemoveSlot(slot);
        }

        public object CaptureState() => GenerateSaveData();

        private List<ItemSaveData> GenerateSaveData()
        {
            var itemSaveDataList = new List<ItemSaveData>();
            foreach (var simpleItem in Inventory.Items)
            {
                var itemSaveData = new ItemSaveData
                {
                    Quantity = simpleItem.Quantity,
                    ItemDetailIdHex = simpleItem.ItemDetailId.ToHexString()
                };
                itemSaveDataList.Add(itemSaveData);
            }

            return itemSaveDataList;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;

            LoadTheInventory((List<ItemSaveData>)state);
        }

        private void LoadTheInventory(List<ItemSaveData> itemSaveDataList)
        {
            _initialItems.Clear();

            foreach (var itemSaveData in itemSaveDataList)
            {
                var itemDetailSerializableGuid = SerializableGuid.FromHexString(itemSaveData.ItemDetailIdHex);
                var itemDetail = ItemDatabase.GetItemDetailById<SimpleItemDetail>(itemDetailSerializableGuid);

                var item = new SimpleItemDetailQuantity
                {
                    ItemDetail = itemDetail,
                    Quantity = itemSaveData.Quantity
                };

                _initialItems.Add(item);
            }
        }

        [Serializable]
        public class ItemSaveData
        {
            public string ItemDetailIdHex;
            public int Quantity;
        }
    }
}
