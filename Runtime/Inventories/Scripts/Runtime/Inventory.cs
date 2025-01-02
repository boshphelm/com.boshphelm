using System;
using System.Collections.Generic;
using System.Linq;
using Boshphelm.Items;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Inventories
{
    public abstract class Inventory<TItem, TItemDetail, TItemDetailQuantity>
        where TItem : Item
        where TItemDetail : ItemDetail
        where TItemDetailQuantity : ItemDetailQuantity<TItemDetail>
    {
        private readonly List<TItem> _items = new List<TItem>();
        public List<TItem> Items => _items;

        public Action<TItem> OnItemAdd = _ => { };
        public Action<TItem> OnItemRemove = _ => { };

        public Action<TItem, int> OnAddToItemStack = (_, _) => { };
        public Action<TItem, int> OnRemoveFromItemStack = (_, _) => { };

        public abstract void Initialize(List<TItemDetailQuantity> initialItemDetailQuantities);
        protected abstract TItem GenerateItem(TItemDetailQuantity itemDetailQuantity);

        public virtual void AddItem(TItem item)
        {
            if (item == null) return;

            if (item.ItemDetail.Stackable && Contains(item.ItemDetail))
            {
                AddToStackableItem(item.ItemDetail, item.Quantity);
            }
            else
            {
                CreateItem(item);
            }

            var itemDetail = item.ItemDetail as TItemDetail;
            if (itemDetail == null)
            {
                Debug.LogError($"Item {item.ItemDetail} is not a TItemDetail");
                return;
            }
        }

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
        }
        private void AddToStackableItem(TItemDetailQuantity itemDetailQuantity)
        {
            AddToStackableItem(itemDetailQuantity.ItemDetail, itemDetailQuantity.Quantity);
        }
        private void AddToStackableItem(ItemDetail itemDetail, int quantity)
        {
            var similarItem = GetItemByItemDetailId(itemDetail.Id);
            similarItem.Quantity += quantity;
            OnAddToItemStack.Invoke(similarItem, similarItem.Quantity);
        }
        private void CreateItem(TItemDetailQuantity itemDetailQuantity)
        {
            var generatedItem = GenerateItem(itemDetailQuantity);
            CreateItem(generatedItem);
        }
        private void CreateItem(TItem item)
        {
            _items.Add(item);
            OnItemAdd.Invoke(item);
            Debug.Log("CREATED ITEM : " + item.ItemDetail.DisplayName + ", QUANTITY : " + item.Quantity);
        }
        public void RemoveItem(TItem item)
        {
            bool contains = _items.Contains(item);
            if (!contains) return;

            bool removed = _items.Remove(item);
        }
        public void RemoveItem(TItemDetail itemDetail, int quantity)
        {
            int totalItemQuantityInInventory = GetItemCountByItemDetail(itemDetail);
            if (totalItemQuantityInInventory < quantity) return;

            var foundItems = FindItemsByItemDetailId(itemDetail.Id);
            RemoveFromItems(foundItems, quantity);
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
                OnRemoveFromItemStack.Invoke(item, item.Quantity);
                _items.Remove(item);
                OnItemRemove.Invoke(item);
            }
            else
            {
                item.Quantity -= quantity;
                quantity = 0;
                OnRemoveFromItemStack.Invoke(item, quantity);
            }
        }
        private void RemoveFromNotStackableItem(TItem item, ref int quantity)
        {
            quantity -= 1;
            _items.Remove(item);
            OnItemRemove.Invoke(item);
        }
        public bool Contains(ItemDetail itemDetail) => GetItemByItemDetailId(itemDetail.Id) != null;
        public TItem GetItemByItemDetailId(SerializableGuid itemDetailId) => _items.FirstOrDefault(item => item.ItemDetailId == itemDetailId);
        public bool HasEnoughItem(ItemDetail itemDetail, int quantity) => GetItemCountByItemDetail(itemDetail) >= quantity;
        public int GetItemCountByItemDetail(ItemDetail itemDetail) => FindItemsByItemDetailId(itemDetail.Id).Sum(item => item.Quantity);
        private List<TItem> FindItemsByItemDetailId(SerializableGuid itemDetailId) => _items.FindAll(item => item.ItemDetailId == itemDetailId);

    }
}
