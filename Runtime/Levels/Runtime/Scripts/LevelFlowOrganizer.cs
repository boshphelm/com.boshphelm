using System;
using UnityEngine;

namespace Boshphelm.Levels
{
    public class LevelFlowOrganizer : MonoBehaviour
    {
        private LevelTimer _levelTimer;
        private LevelProgress _levelProgress;

        public LevelTimer LevelTimer => _levelTimer ??= new LevelTimer();
        public LevelProgress LevelProgress => _levelProgress ??= new LevelProgress();

        private bool _levelActive;

        private LevelUnitTracer _levelUnitTracer;
        private LevelUnitSpawner[] _levelUnitSpawners;

        public Action OnLevelComplete = () => { };

        private bool _finishing;

        public void StartLevel(LevelUnitSpawner[] levelUnitSpawners, int levelIndex)
        {
            ResetLevel();

            //_levelEnemyStat = _levelEnemyStatContainer.GetLevelEnemyStatByLevelIndex(levelIndex);

            _levelUnitSpawners = levelUnitSpawners;

            SetLevelEnemyTracerAndLevelProgress();
            //_levelEnemyCommunication = new LevelEnemyCommunication(LootArea, _levelEnemyTracer);
            //_levelEnemyInitializer = new LevelEnemyInitializer(_levelEnemyTracer, _levelEnemyTypePriceChoser, LootArea, _levelEnemyStat);

            _levelActive = true;
        }

        private void SetLevelEnemyTracerAndLevelProgress()
        {
            _levelUnitTracer = new LevelUnitTracer(_levelUnitSpawners);
            LevelProgress.LevelUnitTracer = _levelUnitTracer;

            LevelProgress.Start(_levelUnitTracer.TotalUnitAmount);
            LevelProgress.OnLevelProgressUpdated += OnLevelProgressUpdated;

            //_levelProgressUIController.Init(LevelProgress);
            //_levelProgressUIController.ActivateWaveUI();
        }

        private void OnLevelProgressUpdated(float progress)
        {
            if (progress >= 1f)
            {
                ActivateLevelFinisher();
            }
            // TODO: Update Level Progress UI.
        }

        private void ActivateLevelFinisher()
        {
            /*if (_levelFinisher != null)
            {
                _levelFinisher.OnLevelFinished -= OnLevelFinished;
                _levelFinisher = null;
            }

            _levelFinisher = new LevelFinisher(LevelTimer);
            _levelFinisher.OnLevelFinished += OnLevelFinished;*/
            _finishing = true;
        }

        private void Update()
        {
            if (!_levelActive) return;

            LevelTimer.Tick(Time.deltaTime);
            if (_finishing)
            {
                Debug.Log("LEVEL FINISHING");
            }
            else
            {
                foreach (var levelUnitSpawner in _levelUnitSpawners)
                {
                    levelUnitSpawner.Tick();
                }
            }
        }

        public void StopLevel()
        {
            _levelActive = false;
            //   _levelEnemyCommunication.DisableAllEnemies();
            _levelProgress.Deactivate();
        }

        public void ResetLevel()
        {

            _levelActive = false;

            if (_levelUnitTracer != null) _levelUnitTracer.Reset();

            LevelTimer.Reset();
            LevelProgress.Reset();
            LevelProgress.OnLevelProgressUpdated -= OnLevelProgressUpdated;
        }


    }
}
