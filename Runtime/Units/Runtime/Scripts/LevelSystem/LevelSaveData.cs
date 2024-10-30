using System;

namespace Boshphelm.Units.Level
{
    [Serializable]
    public class LevelSaveData
    {
        public int CurrentLevel;
        public float CurrentExperience;
        public float ExperienceToNextLevel;
    }

    [Serializable]
    public class EnemyLevelSaveData : LevelSaveData
    {
        public float ExperienceValue;
    }
}
