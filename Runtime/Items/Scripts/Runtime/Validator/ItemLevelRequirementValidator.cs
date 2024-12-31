namespace Boshphelm.Items
{
    public class ItemLevelRequirementValidator : IItemValidator<Item>
    {
        private readonly int _userLevel;
        private readonly int _requiredLevel;

        public ItemLevelRequirementValidator(int userLevel, int requiredLevel)
        {
            _userLevel = userLevel;
            _requiredLevel = requiredLevel;
        }

        public bool Validate(Item item) => _userLevel >= _requiredLevel;
        public string GetValidationErrorMessage(Item item) => $"Requires level {_requiredLevel} (Current level: {_userLevel})";
    }
}
