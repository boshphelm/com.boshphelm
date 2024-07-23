using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Stats
{
    public abstract class StatContainer<T> : MonoBehaviour, IStatContainer<T> where T : StatType
    {
        [SerializeField] protected BaseStatContainer<T> baseStatContainer;
        private Dictionary<T, Stat<T>> _stats;
        public int Level { get; private set; }

        protected virtual void Awake()
        {
            GenerateOrUpdateAllStats();
        }

        public void AddModifierStats(BaseStat<T>[] baseStats, object source)
        {
            foreach (var baseStat in baseStats)
            {
                var stat = GetStatByStatType(baseStat.StatType);
                if (stat == null) continue;

                StatModifier modifier = new StatModifier(baseStat.Value, StatModifierType.Flat, source);
                stat.AddModifier(modifier);
            }
        }

        public Stat<T> GetStatByStatType(T statType)
        {
            if (_stats != null && _stats.TryGetValue(statType, out var stat)) return stat;

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
            if (baseStatContainer == null) return;

            var levelBaseStat = baseStatContainer.GetBaseStatsByLevel(Level);

            if (_stats == null) _stats = new Dictionary<T, Stat<T>>();

            foreach (var baseStat in levelBaseStat.BaseStats)
            {
                GenerateOrUpdateStat(baseStat);
            }
        }

        private void GenerateOrUpdateStat(BaseStat<T> baseStat)
        {
            if (baseStat == null) return;

            if (_stats.ContainsKey(baseStat.StatType))
            {
                _stats[baseStat.StatType].UpdateBaseValue(baseStat.Value);
            }
            else
            {
                _stats.Add(baseStat.StatType, new Stat<T>(baseStat.Value, baseStat.StatType));
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