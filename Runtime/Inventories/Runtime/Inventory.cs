using System;
using System.Collections.Generic;
using System.Linq;
using Boshphelm.Items;
using Boshphelm.Save;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Inventories
{
    [RequireComponent(typeof(SaveableEntity))]
    public class Inventory : MonoBehaviour, ISaveable
    {
        [SerializeField] private ItemDetailQuantity[] _startingItems;

        private readonly List<Item> _items = new List<Item>();

        public Action<ItemDetail, int> OnItemAdd = (_, _) => { };
        public Action<ItemDetail, int> OnItemRemove = (_, _) => { };

        private bool _isRestored;

        public void Initialize()
        {
            if (!_isRestored)
            {
                GenerateStartingItems();
            }
        }

        private void GenerateStartingItems()
        {
            foreach (var startingItem in _startingItems)
            {
                var item = startingItem.ItemDetail.Create(startingItem.Quantity);
                _items.Add(item);
            }
        }

        public void AddItem(ItemDetail itemDetail, int quantity, int itemLevel = 0)
        {
            var item = GetItemByItemDetailId(itemDetail.Id);
            if (item != null && item.ItemDetail.Stackable)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new Item(itemDetail, quantity, itemLevel);
                _items.Add(item);
            }

            OnItemAdd.Invoke(itemDetail, quantity);
        }
        public void RemoveItem(ItemDetail itemDetail, int quantity)
        {
            int totalItemQuantityInInventory = GetItemCountByItemDetail(itemDetail);
            if (totalItemQuantityInInventory < quantity) return;

            var foundItems = FindItemsByItemDetailId(itemDetail.Id);

            for (int i = foundItems.Count - 1; i >= 0; i--)
            {
                if (quantity == 0) break;

                var item = foundItems[i];
                if (item.ItemDetail.Stackable)
                {
                    if (quantity >= item.Quantity)
                    {
                        quantity -= item.Quantity;
                        _items.Remove(item);
                    }
                    else
                    {
                        item.Quantity -= quantity;
                        quantity = 0;
                    }
                }
                else
                {
                    quantity -= 1;
                    _items.Remove(item);
                }
            }

            OnItemRemove.Invoke(itemDetail, quantity);
        }
        public Item GetItemByItemDetail(ItemDetail itemDetail) => GetItemByItemDetailId(itemDetail.Id);
        public Item GetItemByItemDetailId(SerializableGuid itemDetailId) => _items.FirstOrDefault(item => item.ItemDetailId == itemDetailId);
        public bool HasEnoughItem(ItemDetail itemDetail, int quantity) => GetItemCountByItemDetail(itemDetail) >= quantity;
        public int GetItemCountByItemDetail(ItemDetail itemDetail) => FindItemsByItemDetailId(itemDetail.Id).Sum(item => item.Quantity);
        private List<Item> FindItemsByItemDetailId(SerializableGuid itemDetailId) => _items.FindAll(item => item.ItemDetailId == itemDetailId);

        public object CaptureState() => GenerateSaveData();

        public List<ItemSaveData> GenerateSaveData()
        {
            var itemSaveDatas = new List<ItemSaveData>();
            foreach (var item in _items)
            {
                var itemSaveData = new ItemSaveData
                {
                    Quantity = item.Quantity,
                    ItemDetailIdHex = item.ItemDetailId.ToHexString()
                };
                itemSaveDatas.Add(itemSaveData);
            }

            return itemSaveDatas;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;

            var itemSaveDataList = (List<ItemSaveData>)state;

            foreach (var itemSaveData in itemSaveDataList)
            {
                var itemDetailSerializableGuid = SerializableGuid.FromHexString(itemSaveData.ItemDetailIdHex);
                var itemDetail = ItemDatabase.GetItemDetailById(itemDetailSerializableGuid);

                var item = itemDetail.Create(itemSaveData.Quantity);
                _items.Add(item);
            }

            _isRestored = true;
        }

    }

    [Serializable]
    public class ItemDetailQuantity
    {
        public ItemDetail ItemDetail;
        public int Quantity;
    }

    [Serializable]
    public class ItemSaveData
    {
        public string ItemDetailIdHex;
        public int Quantity;
    }
}
