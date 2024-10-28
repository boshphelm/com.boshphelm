using UnityEngine;
using Boshphelm.Units.Level;

namespace Boshphelm.Units
{
    public abstract class LevelableEnemy : LevelableEntity 
    {
        [SerializeField] protected float _experienceValue = 50f;
        [SerializeField] protected float _experienceMultiplier = 1.2f;

        public float ExperienceValue => GetExperienceValue();

        protected virtual float GetExperienceValue()
        {
            return _experienceValue * Mathf.Pow(_experienceMultiplier, _currentLevel - 1);
        }

        public override void LevelUp()
        {
            base.LevelUp();
            _experienceValue *= _experienceMultiplier;
        }

        public override object CaptureState()
        {
            return new EnemyLevelSaveData
            {
                CurrentLevel = _currentLevel,
                ExperienceValue = _experienceValue
            };
        }

        public override void RestoreState(object state)
        {
            if (state == null) return;

            var saveData = (EnemyLevelSaveData)state;
            _currentLevel = Mathf.Clamp(saveData.CurrentLevel, 1, MaxLevel);
            _experienceValue = saveData.ExperienceValue;
            _isRestored = true;

            InitializeStats();
        }
    }
}
