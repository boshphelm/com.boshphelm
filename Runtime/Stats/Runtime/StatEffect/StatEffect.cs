namespace Boshphelm.Stats
{
    [System.Serializable]
    public class StatEffect<T> where T : StatType
    {
        public string Description;
        public T StatType;
        public StatModifier StatModifier;

        public StatTypeBonus<T> GetAsBonusData() => new StatTypeBonus<T>(StatType, StatModifier.Value, StatModifier.StatModifierType);
    }
}