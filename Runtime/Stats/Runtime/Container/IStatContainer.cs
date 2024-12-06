namespace Boshphelm.Stats
{
    public interface IStatContainer
    {
        Stat GetStatByStatType(StatType statType);
        void AddModifierStats(BaseStat[] baseStats, object source);
        void RemoveModifiersFromSource(object source);
    }
}
