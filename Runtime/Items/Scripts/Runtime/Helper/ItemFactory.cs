using System.Collections.Generic;
using Boshphelm.Items;

namespace Boshphelm.Items
{
    public abstract class ItemFactory<TItem, TItemDetail, TItemDetailQuantity> where TItem : Item where TItemDetail : ItemDetail where TItemDetailQuantity : ItemDetailQuantity<TItemDetail>
    {
        public abstract TItem CreateItem(TItemDetailQuantity itemDetailQuantity);
        public List<TItem> CreateItems(List<TItemDetailQuantity> itemDetailQuantities)
        {
            var items = new List<TItem>();
            for (int i = 0; i < itemDetailQuantities.Count; i++)
            {
                var newItem = CreateItem(itemDetailQuantities[i]);
                items.Add(newItem);
            }

            return items;
        }
    }
}
