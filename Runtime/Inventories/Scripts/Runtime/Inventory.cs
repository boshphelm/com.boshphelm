using System;
using System.Collections.Generic;
using System.Linq;
using Boshphelm.Items;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Inventories
{
    public abstract class Inventory<TItem, TItemDetail, TItemDetailQuantity> where TItem : Item<TItemDetail> where TItemDetail : ItemDetail where TItemDetailQuantity : ItemDetailQuantity<TItemDetail>
    {
        private readonly List<TItem> _items = new List<TItem>();
        public List<TItem> Items => _items;

        public Action<TItemDetail, int> OnItemAdd = (_, _) => { };
        public Action<TItemDetail, int> OnItemRemove = (_, _) => { };

        public abstract void Initialize(List<TItemDetailQuantity> initialItemDetailQuantities);
        protected abstract TItem GenerateItem(TItemDetailQuantity itemDetailQuantity);

        public virtual void AddItem(TItemDetailQuantity itemDetailQuantity)
        {
            if (itemDetailQuantity == null) return;

            if (Contains(itemDetailQuantity.ItemDetail) && itemDetailQuantity.ItemDetail.Stackable)
            {
                AddToStackableItem(itemDetailQuantity);
            }
            else
            {
                CreateItem(itemDetailQuantity);
            }

            OnItemAdd.Invoke(itemDetailQuantity.ItemDetail, itemDetailQuantity.Quantity);
        }
        private void AddToStackableItem(TItemDetailQuantity itemDetailQuantity)
        {
            var similarItem = GetItemByItemDetailId(itemDetailQuantity.ItemDetail.Id);
            similarItem.Quantity += itemDetailQuantity.Quantity;
        }
        private void CreateItem(TItemDetailQuantity itemDetailQuantity)
        {
            var generatedItem = GenerateItem(itemDetailQuantity);
            _items.Add(generatedItem);
            Debug.Log("CREATED ITEM : " + generatedItem.ItemDetail.DisplayName + ", QUANTITY : " + generatedItem.Quantity);
        }

        public void RemoveItem(TItemDetail itemDetail, int quantity)
        {
            int totalItemQuantityInInventory = GetItemCountByItemDetail(itemDetail);
            if (totalItemQuantityInInventory < quantity) return;

            var foundItems = FindItemsByItemDetailId(itemDetail.Id);
            RemoveFromItems(foundItems, quantity);

            OnItemRemove.Invoke(itemDetail, quantity);
        }
        private void RemoveFromItems(List<TItem> items, int quantity)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (quantity == 0) break;

                var item = items[i];
                RemoveFromItem(item, ref quantity);
            }
        }

        private void RemoveFromItem(TItem item, ref int quantity)
        {
            if (item.ItemDetail.Stackable)
            {
                RemoveFromStackableItem(item, ref quantity);
            }
            else
            {
                RemoveFromNotStackableItem(item, ref quantity);
            }
        }
        private void RemoveFromStackableItem(TItem item, ref int quantity)
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
        private void RemoveFromNotStackableItem(TItem item, ref int quantity)
        {
            quantity -= 1;
            _items.Remove(item);
        }
        public bool Contains(ItemDetail itemDetail) => GetItemByItemDetailId(itemDetail.Id) != null;
        public TItem GetItemByItemDetailId(SerializableGuid itemDetailId) => _items.FirstOrDefault(item => item.ItemDetailId == itemDetailId);
        public bool HasEnoughItem(ItemDetail itemDetail, int quantity) => GetItemCountByItemDetail(itemDetail) >= quantity;
        public int GetItemCountByItemDetail(ItemDetail itemDetail) => FindItemsByItemDetailId(itemDetail.Id).Sum(item => item.Quantity);
        private List<TItem> FindItemsByItemDetailId(SerializableGuid itemDetailId) => _items.FindAll(item => item.ItemDetailId == itemDetailId);

    }
}
