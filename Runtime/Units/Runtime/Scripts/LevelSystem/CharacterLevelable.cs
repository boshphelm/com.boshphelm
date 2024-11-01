using UnityEngine; 
using Boshphelm.Units.Level; 
using Boshphelm.Utility;

namespace Boshphelm.Units
{
    public class CharacterLevelable : LevelableEntity
    {
        [SerializeField] private ProgressBar _progressBar;
        protected float _currentExperience; 
        public float CurrentExperience => _currentExperience;
        public float ExperienceToNextLevel => GetExperienceRequiredForNextLevel(); 
        protected override void Awake()
        {
            base.Awake();
            if (!_isRestored) _currentExperience = 0f;
            
            _progressBar.SetValue(0);
            _progressBar.SetMaxValue(ExperienceToNextLevel);
        }
        private void Update() {
            if(Input.GetKeyDown(KeyCode.X))
            {
                GainExperience(20);
            }
        }

        protected float GetExperienceRequiredForNextLevel()
        {
            if (_statContainer == null || _statContainer.BaseStatContainer == null) return 0;
            return _statContainer.BaseStatContainer.GetRequiredExperienceForLevel(_currentLevel + 1);
        }

        public override void GainExperience(float exp)
        {
            if (_currentLevel >= MaxLevel) return;

            _currentExperience += exp; 
            _progressBar.SetValue(_currentExperience);
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            float requiredExp = GetExperienceRequiredForNextLevel();
            
            while (_currentExperience >= requiredExp && _currentLevel < MaxLevel)
            {
                _currentExperience -= requiredExp;
                LevelUp();
                requiredExp = GetExperienceRequiredForNextLevel();

                _progressBar.SetValue(0);
                _progressBar.SetMaxValue(ExperienceToNextLevel);
            }
        }

        public float GetProgressToNextLevel()
        {
            float requiredExp = GetExperienceRequiredForNextLevel();
            if (requiredExp <= 0) return 1f;
            return _currentExperience / requiredExp;
        }

        public string GetLevelProgressText()
        {
            if (_currentLevel >= MaxLevel)
                return $"Level {_currentLevel} (MAX)";
            
            return $"Level {_currentLevel} ({_currentExperience:F0}/{GetExperienceRequiredForNextLevel():F0} XP)";
        }

        public override object CaptureState()
        {
            return new LevelSaveData
            {
                CurrentLevel = _currentLevel,
                CurrentExperience = _currentExperience
            };
        }

        public override void RestoreState(object state)
        {
            if (state == null) return;

            var saveData = (LevelSaveData)state;
            _currentLevel = Mathf.Clamp(saveData.CurrentLevel, 1, MaxLevel);
            _currentExperience = saveData.CurrentExperience;
            _isRestored = true;

            InitializeStats();
        }
    }
}