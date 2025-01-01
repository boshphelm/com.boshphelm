using Boshphelm.Items;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemTypeValidator : ItemTypeValidator<SimpleItem>
    {
        public SimpleItemTypeValidator(ItemType[] allowedTypes) : base(allowedTypes) { }
        public SimpleItemTypeValidator(ItemType allowedType) : base(allowedType) { }
    }
}
