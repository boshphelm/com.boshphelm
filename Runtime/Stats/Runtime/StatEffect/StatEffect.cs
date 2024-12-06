namespace Boshphelm.Stats
{
    [System.Serializable]
    public class StatEffect
    {
        public string Description;
        public StatType StatType;
        public StatModifier StatModifier;

        public StatTypeBonus<StatType> GetAsBonusData() => new StatTypeBonus<StatType>(StatType, StatModifier.Value, StatModifier.StatModifierType);
    }
}
