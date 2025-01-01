using Boshphelm.Items;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemLevelRequirementValidator : ItemLevelRequirementValidator<SimpleItem>
    {

        public SimpleItemLevelRequirementValidator(int userLevel, int requiredLevel) : base(userLevel, requiredLevel) { }
    }
}
