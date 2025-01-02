using Boshphelm.Items;
using Boshphelm.ItemSlot;
using UnityEngine;
using UnityEngine.Pool;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public abstract class ItemSlotPool<TItem, TItemSlot>
        where TItem : Item
        where TItemSlot : ItemSlotBase<TItem>
    {
        private readonly int _initialCapacity;
        private readonly int _maxCapacity;

        private IObjectPool<TItemSlot> _pool;

        protected ItemSlotPool(int initialCapacity, int maxCapacity)
        {
            _initialCapacity = initialCapacity;
            _maxCapacity = maxCapacity;

            GeneratePool();
        }

        private void GeneratePool()
        {
            _pool = new ObjectPool<TItemSlot>(CreateSlot, GetSlot, ReleaseSlot, DestroySlot, true, _initialCapacity, _maxCapacity);
        }

        protected abstract TItemSlot CreateSlot();
        private void GetSlot(TItemSlot slot)
        {
            if (!slot.IsEmpty)
            {
                Debug.LogError("GETTING SLOT BUT IT IS NOT EMPTY ! ITEM : " + slot.Item.ItemDetail, slot.Item.ItemDetail);
            }
        }
        private void ReleaseSlot(TItemSlot slot)
        {
            if (!slot.IsEmpty)
            {
                Debug.LogError("SLOT RELEASED BUT IT IS NOT EMPTY ! ITEM : " + slot.Item.ItemDetail, slot.Item.ItemDetail);
            }

            slot.ClearSlot();
        }
        private void DestroySlot(TItemSlot simpleItemSlot)
        {

        }

        public TItemSlot GetFromPool() => _pool.Get();
        public void ReturnToPool(TItemSlot slot) => _pool.Release(slot);
    }
}
