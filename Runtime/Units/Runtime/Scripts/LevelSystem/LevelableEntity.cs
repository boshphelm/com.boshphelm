using UnityEngine; 
using Boshphelm.Save;
using Boshphelm.Units.Level;

namespace Boshphelm.Units
{
    public abstract class LevelableEntity : MonoBehaviour, ISaveable
    {
        [SerializeField] protected int _startingLevel = 1;
        [SerializeField] protected UnitStatContainer _statContainer;
        
        protected int _currentLevel;
        protected bool _isRestored;
        
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => GetMaxLevelFromStatContainer();

        protected virtual void Awake()
        {
            _currentLevel = _startingLevel;
            if (!_isRestored) InitializeStats();
        }

        protected virtual void InitializeStats()
        {
            if (_statContainer == null) _statContainer = GetComponent<UnitStatContainer>();
            
            ValidateStartingLevel();
            _statContainer.SetLevel(_currentLevel);
        }

        protected virtual void ValidateStartingLevel()
        {
            int maxLevel = GetMaxLevelFromStatContainer();
            if (_startingLevel > maxLevel)
            {
                Debug.LogWarning($"Starting level {_startingLevel} is greater than max level {maxLevel}. Setting to max level.");
                _startingLevel = maxLevel;
                _currentLevel = _startingLevel;
            }
        }

        protected virtual int GetMaxLevelFromStatContainer()
        {
            if (_statContainer == null || _statContainer.BaseStatContainer == null)
            {
                Debug.LogError("StatContainer or BaseStatContainer is not assigned!");
                return 1;
            }
            return _statContainer.BaseStatContainer.MaxLevel;
        }

        public virtual void GainExperience(float exp)
        {
            // Override in child classes
        }

        public virtual void LevelUp()
        {
            if (_currentLevel >= MaxLevel) return;
            
            _currentLevel++;
            OnLevelUp();
            UpdateStats();
        }

        protected virtual void OnLevelUp()
        {
            // Override to add custom level up behavior
        }

        protected virtual void UpdateStats()
        {
            _statContainer.SetLevel(_currentLevel);
        }

        public virtual object CaptureState()
        {
            return new LevelSaveData
            {
                CurrentLevel = _currentLevel
            };
        }

        public virtual void RestoreState(object state)
        {
            if (state == null) return;

            var saveData = (LevelSaveData)state;
            _currentLevel = Mathf.Clamp(saveData.CurrentLevel, 1, MaxLevel);
            _isRestored = true;
            
            InitializeStats();
        }
    }
}
