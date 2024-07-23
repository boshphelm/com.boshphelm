namespace Boshphelm.Stats
{
    public interface IStatListener
    {
        void OnBaseValueChanged(StatType statType, float newBaseValue);
        void OnTotalValueChanged(StatType statType, float newTotalValue);
    }
}