namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemSlotPool : ItemSlotPool<SimpleItem, SimpleItemSlot>
    {
        public SimpleItemSlotPool(int initialCapacity, int maxCapacity) : base(initialCapacity, maxCapacity)
        {
        }

        protected override SimpleItemSlot CreateSlot() => new SimpleItemSlot();
    }
}
