using System;
using System.Collections.Generic;
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

        [Header("Listening To...")] [SerializeField] private ItemDetailIntEventChannel _onItemAdd;
        [SerializeField] private ItemDetailIntEventChannel _onItemRemove;
        [SerializeField] private ItemReturnItemDetailEventChannel _onItemRequestedByItemDetail;
        [SerializeField] private ItemReturnSerializableGuidEventChannel _onItemRequestedByItemDetailSerializableGuid;
        [SerializeField] private BoolReturnItemDetailIntEventChannel _hasEnoughItem;
        [SerializeField] private IntReturnItemDetailEventChannel _itemCountRequested;
        [SerializeField] private VoidEventChannel _onNewGameStarted;

        private InventoryController _inventoryController;

        private void OnEnable()
        {
            _onItemAdd.OnEventRaise += OnItemAdd;
            _onItemRemove.OnEventRaise += OnItemRemove;
            _onItemRequestedByItemDetail.OnRaiseEvent += OnItemRequestedByItemDetail;
            _onItemRequestedByItemDetailSerializableGuid.OnRaiseEvent += OnItemRequestedByItemDetailId;
            _hasEnoughItem.OnRaiseEvent += HasEnoughItem;
            _itemCountRequested.OnRaiseEvent += ItemCountRequested;
            _onNewGameStarted.OnEventRaise += OnNewGameStarted;
        }

        private void OnDisable()
        {
            _onItemAdd.OnEventRaise -= OnItemAdd;
            _onItemRemove.OnEventRaise -= OnItemRemove;
            _onItemRequestedByItemDetail.OnRaiseEvent -= OnItemRequestedByItemDetail;
            _onItemRequestedByItemDetailSerializableGuid.OnRaiseEvent -= OnItemRequestedByItemDetailId;
            _hasEnoughItem.OnRaiseEvent -= HasEnoughItem;
            _itemCountRequested.OnRaiseEvent -= ItemCountRequested;
            _onNewGameStarted.OnEventRaise -= OnNewGameStarted;
        }


        private void OnItemAdd(ItemDetail itemDetail, int quantity) => _inventoryController.AddItem(itemDetail, quantity);
        private void OnItemRemove(ItemDetail itemDetail, int quantity) => _inventoryController.RemoveItem(itemDetail, quantity);
        private Item OnItemRequestedByItemDetail(ItemDetail itemDetail) => _inventoryController.GetByItemDetail(itemDetail);
        private Item OnItemRequestedByItemDetailId(SerializableGuid itemDetailId) => _inventoryController.GetItemByItemDetailId(itemDetailId);
        private bool HasEnoughItem(ItemDetail itemDetail, int quantity) => _inventoryController.HasEnoughItem(itemDetail, quantity);
        private int ItemCountRequested(ItemDetail itemDetail) => _inventoryController.GetItemCountByItemDetail(itemDetail);
 


        private void OnNewGameStarted()
        {
            Initialize(GenerateStartingItems());
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

        private void Initialize(List<Item> items)
        {
            _inventoryController = new InventoryController(items);
        }

        public object CaptureState() => _inventoryController.GenerateSaveData();

        public void RestoreState(object state)
        {
            if (state == null) return;

            var loadedItems = new List<Item>();
            var itemSaveDataList = (List<ItemSaveData>)state;

            foreach (ItemSaveData itemSaveData in itemSaveDataList)
            {
                SerializableGuid itemDetailSerializableGuid = SerializableGuid.FromHexString(itemSaveData.ItemDetailIdHex);
                ItemDetail itemDetail = ItemDatabase.GetItemDetailById(itemDetailSerializableGuid);

                Item item = itemDetail.Create(itemSaveData.Quantity);
                loadedItems.Add(item);
            }

            Initialize(loadedItems);
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