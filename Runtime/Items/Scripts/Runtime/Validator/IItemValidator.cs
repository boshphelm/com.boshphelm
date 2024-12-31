namespace Boshphelm.Items
{
    public interface IItemValidator<TItem, TItemDetail> where TItem : Item<TItemDetail> where TItemDetail : ItemDetail
    {
        bool Validate(TItem item);
        string GetValidationErrorMessage(TItem item);
    }
}
