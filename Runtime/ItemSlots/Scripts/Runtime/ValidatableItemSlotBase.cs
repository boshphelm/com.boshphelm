using System.Collections.Generic;
using System.Linq;
using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.ItemSlot
{
    public abstract class ValidatableItemSlotBase<TItem> : ItemSlotBase<TItem> where TItem : Item
    {
        protected readonly List<IItemValidator<TItem>> validators = new List<IItemValidator<TItem>>();

        public void AddValidator(IItemValidator<TItem> validator)
        {
            validators.Add(validator);
        }

        public override bool CanAcceptItem(TItem item)
        {
            bool canAccept = validators.All(validator => validator.Validate(item));
            if (!canAccept)
            {
                Debug.LogError($"Cannot Set Item : {GetValidationsErrorMessage(item)}");
            }

            return canAccept;
        }

        public string GetValidationsErrorMessage(TItem item)
        {
            var errors = validators
                .Where(validator => !validator.Validate(item))
                .Select(validator => validator.GetValidationErrorMessage(item));

            return string.Join("\n", errors);
        }
    }
}
