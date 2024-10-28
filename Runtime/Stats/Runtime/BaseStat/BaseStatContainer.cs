using System.Linq;
using UnityEngine;

namespace Boshphelm.Stats
{ 
    public abstract class BaseStatContainer<T> : ScriptableObject where T : StatType
    {
        [SerializeField] private LevelBaseStat<T>[] _levelBaseStats;

        public int MaxLevel => _levelBaseStats != null ? _levelBaseStats.Length : 1;

        public BaseStat<T> GetBaseStatByLevelAndType(int level, T type)
        {
            var levelBaseStat = GetBaseStatsByLevel(level);
            return levelBaseStat.GetBaseStat(type);
        }

        public LevelBaseStat<T> GetBaseStatsByLevel(int level)
        {
            level = Mathf.Clamp(level, 1, MaxLevel);

            foreach (var levelBaseStat in _levelBaseStats)
            {
                if (!levelBaseStat.IsSameLevel(level)) continue;
                return levelBaseStat;
            }

            return _levelBaseStats[^1];
        }

        public float GetRequiredExperienceForLevel(int level)
        {
            if (level <= 0 || level > MaxLevel) return 0;
            return _levelBaseStats[level - 1].RequiredExperience;
        }

#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button("Generate Default Level Requirements")]
        private void GenerateDefaultLevelRequirements()
        {
            if (_levelBaseStats == null || _levelBaseStats.Length == 0)
            {
                Debug.LogWarning("No level stats found. Create level stats first.");
                return;
            }

            float baseExperience = 100f;
            float multiplier = 1.5f;

            for (int i = 0; i < _levelBaseStats.Length; i++)
            {
                _levelBaseStats[i].RequiredExperience = baseExperience;
                baseExperience *= multiplier;
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }

    [System.Serializable]
    public class LevelBaseStat<T> where T : StatType
    {
        public int Level;
        public float RequiredExperience;
        public BaseStat<T>[] BaseStats;

        public BaseStat<T> GetBaseStat(T statType)
        {
            return BaseStats.FirstOrDefault(baseStat => baseStat.HasSameStatType(statType));
        }

        public bool IsSameLevel(int level) => Level == level;
    }

    [System.Serializable]
    public class BaseStat<T> where T : StatType
    {
        public T StatType;
        public float Value;

        public bool HasSameStatType(T statType) => StatType == statType;
    }
}