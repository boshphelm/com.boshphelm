using Boshphelm.Items;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleCompositeValidator : CompositeValidator<SimpleItem>
    {
        public SimpleCompositeValidator(params IItemValidator<SimpleItem>[] validators) : base(validators)
        {
        }
    }
}
