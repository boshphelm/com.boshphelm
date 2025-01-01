namespace Boshphelm.Items
{
    public class ItemLevelRequirementValidator<TItem> : IItemValidator<TItem> where TItem : Item
    {
        private readonly int _userLevel;
        private readonly int _requiredLevel;

        public ItemLevelRequirementValidator(int userLevel, int requiredLevel)
        {
            _userLevel = userLevel;
            _requiredLevel = requiredLevel;
        }

        public bool Validate(TItem item) => _userLevel >= _requiredLevel;
        public string GetValidationErrorMessage(TItem item) => $"Requires level {_requiredLevel} (Current level: {_userLevel})";
    }
}
