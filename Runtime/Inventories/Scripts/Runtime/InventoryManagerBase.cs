using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.Inventories
{
    public abstract class InventoryManagerBase<TItem, TItemDetail, TItemDetailQuantity> : MonoBehaviour
        where TItem : Item
        where TItemDetail : ItemDetail
        where TItemDetailQuantity : ItemDetailQuantity<TItemDetail>
    {
        protected abstract Inventory<TItem, TItemDetail, TItemDetailQuantity> Inventory { get; }

        public abstract void Initialize();

        public void AddItem(TItemDetailQuantity itemDetailQuantity) { Inventory.AddItem(itemDetailQuantity); }
        public void AddItem(TItem item) { Inventory.AddItem(item); }

        public bool HasEnoughItem(TItemDetail itemDetail, int quantity = 1) => Inventory.HasEnoughItem(itemDetail, quantity);


        public void RemoveItem(TItem item) => Inventory.RemoveItem(item);
        public void RemoveItem(TItemDetail simpleItemDetail, int quantity = 1) => Inventory.RemoveItem(simpleItemDetail, quantity);
    }
}
