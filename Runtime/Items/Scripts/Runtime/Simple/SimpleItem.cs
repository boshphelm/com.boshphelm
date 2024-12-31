namespace Boshphelm.Items
{
    public class SimpleItem : Item<SimpleItemDetail>
    {
        public SimpleItem(SimpleItemDetail itemDetail, int quantity = 1) : base(itemDetail, quantity)
        {
        }
    }
}
