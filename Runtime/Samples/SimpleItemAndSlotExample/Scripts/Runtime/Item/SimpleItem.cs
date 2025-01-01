using Boshphelm.Items;
namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItem : Item
    {
        public SimpleItem(SimpleItemDetail itemDetail, int quantity = 1) : base(itemDetail, quantity)
        {
        }
    }
}
