using System.Collections.Generic;
using Boshphelm.Inventories;
using Boshphelm.Items;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleInventory : Inventory<SimpleItem, SimpleItemDetail, SimpleItemDetailQuantity>
    {
        private readonly SimpleItemFactory _simpleItemFactory = new SimpleItemFactory();

        public override void Initialize(List<SimpleItemDetailQuantity> initialItemDetailQuantities)
        {
            for (int i = 0; i < initialItemDetailQuantities.Count; i++)
            {
                AddItem(initialItemDetailQuantities[i]);
            }
        }
        protected override SimpleItem GenerateItem(SimpleItemDetailQuantity itemDetailQuantity) => _simpleItemFactory.CreateItem(itemDetailQuantity);
    }
}
