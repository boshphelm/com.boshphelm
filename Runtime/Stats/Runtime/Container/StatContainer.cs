using System.Collections.Generic;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Stats
{
    public abstract class StatContainer<T> : MonoBehaviour, IStatContainer<T> where T : StatType
    {
        [SerializeField] private BaseStatContainer<T> baseStatContainer;
        public BaseStatContainer<T> BaseStatContainer => baseStatContainer;
        private Dictionary<SerializableGuid, Stat<T>> _stats;
        public int Level { get; private set; }

        public void Initialize()
        {
            GenerateOrUpdateAllStats();
        }

        public void AddModifierStats(BaseStat<T>[] baseStats, object source)
        {
            foreach (var baseStat in baseStats)
            {
                var stat = GetStatByStatType(baseStat.StatType);
                if (stat == null) continue;

                var modifier = new StatModifier(baseStat.Value, StatModifierType.Flat, source);
                stat.AddModifier(modifier);
            }
        }

        public Stat<T> GetStatByStatType(T statType)
        {
            if (_stats != null && _stats.TryGetValue(statType.Id, out var stat)) return stat;

            return null;
        }

        public void RemoveModifiersFromSource(object source)
        {
            foreach (var stat in _stats)
            {
                stat.Value.RemoveSourceModifiers(source);
            }
        }

        public void SetLevel(int level)
        {
            Level = level;
            GenerateOrUpdateAllStats();
        }

        private void GenerateOrUpdateAllStats()
        {
            if (BaseStatContainer == null) return;

            var levelBaseStat = BaseStatContainer.GetBaseStatsByLevel(Level);

            if (_stats == null) _stats = new Dictionary<SerializableGuid, Stat<T>>();

            foreach (var baseStat in levelBaseStat.BaseStats)
            {
                GenerateOrUpdateStat(baseStat);
            }
        }

        private void GenerateOrUpdateStat(BaseStat<T> baseStat)
        {
            if (baseStat == null) return;

            if (_stats.ContainsKey(baseStat.StatType.Id))
            {
                _stats[baseStat.StatType.Id].UpdateBaseValue(baseStat.Value);
            }
            else
            {
                _stats.Add(baseStat.StatType.Id, new Stat<T>(baseStat.Value, baseStat.StatType));
            }
        }

        public void RegisterListener(T statType, IStatListener listener)
        {
            var stat = GetStatByStatType(statType);

            stat?.RegisterValueListener(listener);
        }

        public void UnregisterListener(T statType, IStatListener listener)
        {
            var stat = GetStatByStatType(statType);

            stat?.UnregisterValueListener(listener);
        }
    }
}
