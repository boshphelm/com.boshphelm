using UnityEngine;
using Boshphelm.Units.Level;

namespace Boshphelm.Units
{
    public abstract class LevelablePlayer : LevelableEntity
    {
        [SerializeField] protected float _experienceToNextLevel = 100f;
        [SerializeField] protected float _experienceMultiplier = 1.5f;
        
        protected float _currentExperience;
        
        public float CurrentExperience => _currentExperience;
        public float ExperienceToNextLevel => _experienceToNextLevel;

        protected override void Awake()
        {
            base.Awake();
            if (!_isRestored)
            {
                _currentExperience = 0f;
            }
        }

        public override void GainExperience(float exp)
        {
            if (_currentLevel >= MaxLevel) return;

            _currentExperience += exp;
            
            while (_currentExperience >= _experienceToNextLevel && _currentLevel < MaxLevel)
            {
                _currentExperience -= _experienceToNextLevel;
                LevelUp();
                _experienceToNextLevel *= _experienceMultiplier;
            }
        }

        public override object CaptureState()
        {
            return new LevelSaveData
            {
                CurrentLevel = _currentLevel,
                CurrentExperience = _currentExperience,
                ExperienceToNextLevel = _experienceToNextLevel
            };
        }

        public override void RestoreState(object state)
        {
            if (state == null) return;

            var saveData = (LevelSaveData)state;
            _currentLevel = Mathf.Clamp(saveData.CurrentLevel, 1, MaxLevel);
            _currentExperience = saveData.CurrentExperience;
            _experienceToNextLevel = saveData.ExperienceToNextLevel;
            _isRestored = true;

            InitializeStats();
        }
    }
}
