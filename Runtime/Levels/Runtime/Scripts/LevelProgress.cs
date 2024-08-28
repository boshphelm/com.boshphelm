using Boshphelm.Units;
using Boshphelm.Utility;

namespace Boshphelm.Levels
{
    public class LevelProgress : IProgress
    {
        private int _totalUnitCount;
        private int _killedUnitCount;
        private int _escapedUnitCount;

        private bool _active;

        public float Progress => (float)(_killedUnitCount + _escapedUnitCount) / _totalUnitCount;

        public System.Action<float> OnLevelProgressUpdated = _ => { };

        private LevelUnitTracer _levelUnitTracer;

        public LevelUnitTracer LevelUnitTracer
        {
            set
            {
                if (_levelUnitTracer != null && _levelUnitTracer != value)
                {
                    _levelUnitTracer.onUnitDead -= OnUnitDead;
                    _levelUnitTracer = null;
                }

                _levelUnitTracer = value;
                _levelUnitTracer.onUnitDead += OnUnitDead;
            }
        }

        public void Start(int totalUnitCount)
        {
            _active = true;
            _totalUnitCount = totalUnitCount;
            _killedUnitCount = 0;
            _escapedUnitCount = 0;
            OnLevelProgressUpdated.Invoke(Progress);
        }

        public void OnUnitDead(LevelUnitSpawner levelUnitSpawner, Unit unit)
        {
            if (!_active) return;

            _killedUnitCount++;
            OnLevelProgressUpdated.Invoke(Progress);
            //Debug.Log("UPDATED PROGRESS : " + Progress);
        }

        public void Deactivate()
        {
            _active = false;
        }

        public void Reset()
        {
            _killedUnitCount = 0;
            _escapedUnitCount = 0;
            _totalUnitCount = 1;
            _active = true;
        }
    }
}
