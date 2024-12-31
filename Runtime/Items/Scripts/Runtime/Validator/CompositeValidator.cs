using System.Collections.Generic;
using System.Linq;

namespace Boshphelm.Items
{
    public class CompositeValidator<TItem> : IItemValidator<TItem> where TItem : Item
    {
        private readonly List<IItemValidator<TItem>> _validators;

        public CompositeValidator(params IItemValidator<TItem>[] validators)
        {
            _validators = new List<IItemValidator<TItem>>(validators);
        }

        public bool Validate(TItem item)
        {
            return _validators.All(validator => validator.Validate(item));
        }

        public string GetValidationErrorMessage(TItem item)
        {
            var errors = _validators
                .Where(validator => !validator.Validate(item))
                .Select(validator => validator.GetValidationErrorMessage(item));

            return string.Join("\n", errors);
        }
    }
}
