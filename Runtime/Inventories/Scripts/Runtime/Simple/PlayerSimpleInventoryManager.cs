using System;
using System.Collections.Generic;
using Boshphelm.Items;
using Boshphelm.Save;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Inventories
{
    public class PlayerSimpleInventoryManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private List<SimpleItemDetailQuantity> _initialItems;

        private SimpleInventory _inventory;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
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

            _initialItems.Clear();

            var itemSaveDataList = (List<ItemSaveData>)state;

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
