using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Boshphelm.Units.Level
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = "Boshphelm/Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<LevelRequirement> _levelRequirements = new List<LevelRequirement>();

        public int MaxLevel => _levelRequirements.Count;

        public float GetRequiredExperience(int level)
        {
            if (level <= 0 || level > _levelRequirements.Count) return 0;
            return _levelRequirements[level - 1].RequiredExperience;
        }

#if UNITY_EDITOR
        [Button("Generate Level Requirements")]
        private void GenerateLevelRequirements()
        {
            _levelRequirements.Clear();
            float baseExperience = 10;
            float multiplier = 1.25f;
            float levelLimit = 10;
            for (int i = 1; i <= levelLimit; i++)
            {
                _levelRequirements.Add(new LevelRequirement
                {
                    Level = i,
                    RequiredExperience = baseExperience
                });

                baseExperience *= multiplier;
            }
        }

        [Button("Clear Level Requirements")]
        private void ClearLevelRequirements()
        {
            _levelRequirements.Clear();
        }
#endif
    }

    [System.Serializable]
    public class LevelRequirement
    {
        public int Level;
        [Tooltip("Experience required to reach next level")]
        public float RequiredExperience;
    }
}
