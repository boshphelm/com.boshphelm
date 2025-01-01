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

        private SimpleInventory _inventory;
        protected override Inventory<SimpleItem, SimpleItemDetail, SimpleItemDetailQuantity> Inventory => _inventory;

        public override void Initialize()
        {
            _inventory = new SimpleInventory();
            _inventory.Initialize(_initialItems);

            _initialItems.Clear();
        }

        public object CaptureState() => GenerateSaveData();

        private List<ItemSaveData> GenerateSaveData()
        {
            var itemSaveDatas = new List<ItemSaveData>();
            foreach (var simpleItem in _inventory.Items)
            {
                var itemSaveData = new ItemSaveData
                {
                    Quantity = simpleItem.Quantity,
                    ItemDetailIdHex = simpleItem.ItemDetailId.ToHexString()
                };
                itemSaveDatas.Add(itemSaveData);
            }

            return itemSaveDatas;
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
