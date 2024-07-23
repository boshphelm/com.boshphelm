namespace Boshphelm.Stats
{
    public interface IStatContainer<T> where T : StatType
    {
        Stat<T> GetStatByStatType(T statType);
        void AddModifierStats(BaseStat<T>[] baseStats, object source);
        void RemoveModifiersFromSource(object source);
    }
}