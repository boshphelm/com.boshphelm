namespace Boshphelm.Items.Validator
{
    public class SimpleItemLevelRequirementValidator : IItemValidator<SimpleItem, SimpleItemDetail>
    {
        private readonly int _userLevel;
        private readonly int _requiredLevel;

        public SimpleItemLevelRequirementValidator(int userLevel, int requiredLevel)
        {
            _userLevel = userLevel;
            _requiredLevel = requiredLevel;
        }

        public bool Validate(SimpleItem item) => _userLevel >= _requiredLevel;
        public string GetValidationErrorMessage(SimpleItem item) => $"Requires level {_requiredLevel} (Current level: {_userLevel})";
    }
}
