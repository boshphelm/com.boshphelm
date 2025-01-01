using Boshphelm.Items;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemFactory : ItemFactory<SimpleItem, SimpleItemDetail, SimpleItemDetailQuantity>
    {
        public override SimpleItem CreateItem(SimpleItemDetailQuantity itemDetailQuantity)
        {
            var simpleItem = new SimpleItem(itemDetailQuantity.ItemDetail, itemDetailQuantity.Quantity);

            return simpleItem;
        }
    }
}
