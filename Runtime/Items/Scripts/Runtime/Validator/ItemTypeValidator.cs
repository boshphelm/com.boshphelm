using System;

namespace Boshphelm.Items
{
    public class ItemTypeValidator : IItemValidator<Item>
    {
        private readonly ItemType[] _allowedTypes;

        public ItemTypeValidator(ItemType[] allowedTypes)
        {
            _allowedTypes = allowedTypes;
        }

        public ItemTypeValidator(ItemType allowedType)
        {
            _allowedTypes = new ItemType[] { allowedType };
        }

        public bool Validate(Item item)
        {
            return _allowedTypes == null ||
                _allowedTypes.Length == 0 ||
                Array.Exists(_allowedTypes, t => t.Id == item.ItemDetail.ItemType.Id);
        }
        public string GetValidationErrorMessage(Item item) => $"Invalid item type: {item.ItemDetail.ItemType.DisplayName}, not allowed.";
    }
}
