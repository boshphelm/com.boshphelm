using System;

namespace Boshphelm.Items
{
    public abstract class ItemTypeValidator<TItem> : IItemValidator<TItem> where TItem : Item
    {
        private readonly ItemType[] _allowedTypes;

        protected ItemTypeValidator(ItemType[] allowedTypes)
        {
            _allowedTypes = allowedTypes;
        }

        protected ItemTypeValidator(ItemType allowedType)
        {
            _allowedTypes = new ItemType[] { allowedType };
        }

        public bool Validate(TItem item)
        {
            return _allowedTypes == null ||
                _allowedTypes.Length == 0 ||
                Array.Exists(_allowedTypes, t => t.Id == item.ItemDetail.ItemType.Id);
        }
        public string GetValidationErrorMessage(TItem item) => $"Invalid item type: {item.ItemDetail.ItemType.DisplayName}, not allowed.";
    }
}
