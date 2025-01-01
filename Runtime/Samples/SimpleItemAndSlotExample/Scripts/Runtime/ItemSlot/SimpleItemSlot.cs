using Boshphelm.Items;
using Boshphelm.ItemSlot;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemSlot : ValidatableItemSlotBase<SimpleItem>
    {
        private readonly ItemType[] _allowedItemTypes;

        public SimpleItemSlot(ItemType[] allowedItemTypes)
        {
            _allowedItemTypes = allowedItemTypes;
            InitializeValidators();
        }

        private void InitializeValidators()
        {
            if (_allowedItemTypes == null || _allowedItemTypes.Length == 0) return;

            var simpleItemLevelValidator = new SimpleItemLevelRequirementValidator(0, 10);
            var simpleItemTypeValidator = new SimpleItemTypeValidator(_allowedItemTypes);

            AddValidator(new SimpleCompositeValidator(simpleItemTypeValidator, simpleItemLevelValidator));
        }
    }
}
