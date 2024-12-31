namespace Boshphelm.Items
{
    public interface IItemValidator<TItem> where TItem : Item
    {
        bool Validate(TItem item);
        string GetValidationErrorMessage(TItem item);
    }
}
