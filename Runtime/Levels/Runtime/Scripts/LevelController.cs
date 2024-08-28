using System;
using System.Collections;
using System.Collections.Generic;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Levels
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelFlowOrganizer _levelFlowOrganizer;

        private int _levelIndex;

        public ITimer Timer => _levelFlowOrganizer.LevelTimer;
        public IProgress Progress => _levelFlowOrganizer.LevelProgress;

        public Action<int> OnLevelComplete = _ => { };
        public Action<int> OnLevelStart = _ => { };
        public Action<int> OnLevelFail = _ => { };
        public Action<int> OnLevelIndexUpdated = _ => { };

        public int LevelIndex
        {
            get => _levelIndex;
            private set
            {
                _levelIndex = Mathf.Clamp(value, 0, int.MaxValue);
                OnLevelIndexUpdated.Invoke(_levelIndex);
            }
        }

        public void AddToLevelIndex()
        {
            LevelIndex++;
        }

        public void LevelStart(LevelUnitSpawner[] levelUnitSpawners)
        {
            _levelFlowOrganizer.StartLevel(levelUnitSpawners, LevelIndex);

            OnLevelStart.Invoke(_levelIndex);
        }

        public void LevelComplete()
        {
            OnLevelComplete.Invoke(_levelIndex);
        }

        public void LevelFail()
        {
            OnLevelFail.Invoke(_levelIndex);
        }

        public void StopLevel()
        {
            _levelFlowOrganizer.StopLevel();
        }
    }
}
