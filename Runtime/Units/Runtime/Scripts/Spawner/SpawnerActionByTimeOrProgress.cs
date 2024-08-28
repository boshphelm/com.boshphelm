using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Units
{
    public class SpawnerActionByTimeOrProgress : UnitSpawnerAction
    {
        private readonly ITimer _timer;
        private readonly IProgress _progress;

        private readonly float _targetProgress;
        private bool _achievedToProgress;

        private readonly float _eachTimeToSpawn;
        private float _timeToNextSpawn;

        private readonly float _delay;
        private float _delayTime;
        private bool _achievedToDelayTime;

        private bool IsTimerReachedToNextSpawnTime => _timer.Time >= _timeToNextSpawn;

        public SpawnerActionByTimeOrProgress(GameObject unitPrefab, UnitSpawner unitSpawner, int amount, ITimer timer, IProgress progress, float totalTimeToSpawn, float targetProgress, float delay) : base(unitPrefab, unitSpawner, amount)
        {
            _timer = timer;
            _progress = progress;

            _targetProgress = targetProgress;

            _delay = delay;
            _eachTimeToSpawn = totalTimeToSpawn / amount;

            UpdateReachedProgressToSpawn();
        }


        public override void Tick()
        {
            if (done) return;

            if (!_achievedToProgress)
            {
                UpdateReachedProgressToSpawn();
                return;
            }

            if (!_achievedToDelayTime)
            {
                UpdateReachedToDelayTime();
                return;
            }

            if (IsTimerReachedToNextSpawnTime)
            {
                SpawnUnit();
            }
        }

        private void UpdateReachedProgressToSpawn()
        {
            _achievedToProgress = _progress.Progress >= _targetProgress;
            if (!_achievedToProgress) return;

            _delayTime = _timer.Time + _delay;
        }

        private void UpdateReachedToDelayTime()
        {
            _achievedToDelayTime = _timer.Time >= _delayTime;
            if (!_achievedToDelayTime) return;

            UpdateNextSpawnTime();
        }


        public override void SpawnUnit()
        {
            base.SpawnUnit();

            if (HasSpawnerReachedToTotalSpawnAmount)
            {
                Done();
            }
            else
            {
                UpdateNextSpawnTime();
            }
        }

        private void UpdateNextSpawnTime()
        {
            _timeToNextSpawn = _timer.Time + _eachTimeToSpawn;
        }
    }
}
