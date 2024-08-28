using System.Collections.Generic;
using Boshphelm.Units;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Levels
{
    public readonly struct LevelUnitSpawnerGenerator
    {
        private readonly UnitWaveInformation[] _unitWaveInformations;

        private readonly UnitSpawner[] _unitSpawners;
        private readonly ITimer _timer;
        private readonly IProgress _progress;

        private readonly Dictionary<UnitSpawnDetail, int> _totalUnitAmount;
        private readonly Dictionary<UnitSpawnDetail, int> _placedUnitAmount;

        private readonly Dictionary<LevelUnitSpawner, Dictionary<UnitSpawnDetail, UnitSpawnerAdaptor>> _placedLevelUnitSpawners;

        public LevelUnitSpawnerGenerator(UnitWaveInformationContainer unitWaveInformationContainer, UnitSpawner[] unitSpawners, ITimer timer, IProgress progress)
        {
            _unitSpawners = unitSpawners;
            _unitWaveInformations = unitWaveInformationContainer.UnitWaveInformationList;
            _timer = timer;
            _progress = progress;

            _totalUnitAmount = new Dictionary<UnitSpawnDetail, int>();
            _placedUnitAmount = new Dictionary<UnitSpawnDetail, int>();

            _placedLevelUnitSpawners = new Dictionary<LevelUnitSpawner, Dictionary<UnitSpawnDetail, UnitSpawnerAdaptor>>();

            InitializeTotalAndPlacedAmounts();
        }

        private void InitializeTotalAndPlacedAmounts()
        {
            foreach (var unitWaveInformation in _unitWaveInformations)
            {
                foreach (var unitSpawnDetail in unitWaveInformation.UnitSpawnDetails)
                {
                    _totalUnitAmount.Add(unitSpawnDetail, unitSpawnDetail.Amount);
                    _placedUnitAmount.Add(unitSpawnDetail, 0);
                }
            }
        }

        public LevelUnitSpawner[] Generate()
        {
            var levelUnitSpawners = GenerateUnitWaveAdaptorsToLevelUnitSpawners(); //LevelUnitSpawners();

            return levelUnitSpawners;
        }


        private LevelUnitSpawner[] GenerateUnitWaveAdaptorsToLevelUnitSpawners()
        {
            var levelUnitSpawners = new List<LevelUnitSpawner>();
            foreach (var unitWaveAdaptor in _unitWaveInformations)
            {
                var waveLevelUnitSpawners = GenerateLevelUnitSpawners(_unitSpawners, unitWaveAdaptor);
                UnitSpawnAmountControl(waveLevelUnitSpawners, unitWaveAdaptor.UnitSpawnDetails);
                levelUnitSpawners.AddRange(waveLevelUnitSpawners);
            }

            return levelUnitSpawners.ToArray();
        }

        private LevelUnitSpawner[] GenerateLevelUnitSpawners(UnitSpawner[] unitSpawners, UnitWaveInformation unitWaveAdaptor)
        {
            int unitSpawnPointCount = unitWaveAdaptor.SpawnerCount;
            if (unitSpawnPointCount > unitSpawners.Length) unitSpawnPointCount = unitSpawners.Length;

            var levelUnitSpawners = new LevelUnitSpawner[unitSpawnPointCount];

            float spawnRateForPerUnitSpawnPoint = 1f / unitSpawnPointCount;
            //Debug.Log("SPAWN RATE FOR PER Unit SPAWN POINT : " + spawnRateForPerUnitSpawnPoint);

            var unitSpawnPointIndexes = new List<int>();
            for (int i = 0; i < _unitSpawners.Length; i++)
            {
                unitSpawnPointIndexes.Add(i);
            }

            for (int i = 0; i < unitSpawnPointCount; i++)
            {
                int randomUnitSpawnerPointIndex = Random.Range(0, unitSpawnPointIndexes.Count);
                var unitSpawner = unitSpawners[unitSpawnPointIndexes[randomUnitSpawnerPointIndex]];
                unitSpawnPointIndexes.RemoveAt(randomUnitSpawnerPointIndex);

                var placedUnitSpawnDetails = new Dictionary<UnitSpawnDetail, UnitSpawnerAdaptor>();

                levelUnitSpawners[i] = GenerateLevelUnitSpawner(unitSpawner, unitWaveAdaptor, spawnRateForPerUnitSpawnPoint, placedUnitSpawnDetails);

                _placedLevelUnitSpawners.Add(levelUnitSpawners[i], placedUnitSpawnDetails);
            }

            return levelUnitSpawners;
        }

        private LevelUnitSpawner GenerateLevelUnitSpawner(UnitSpawner unitSpawner, UnitWaveInformation unitWaveAdaptor, float spawnRateForPerUnitSpawnPoint,
            Dictionary<UnitSpawnDetail, UnitSpawnerAdaptor> placedUnitSpawnDetails)
        {
            var unitSpawnerAdaptors = GenerateUnitSpawnerAdaptors(unitSpawner, unitWaveAdaptor, spawnRateForPerUnitSpawnPoint, placedUnitSpawnDetails);
            var unitSpawnerBaseActions = GenerateUnitSpawnerBaseActions(unitSpawner, unitSpawnerAdaptors);

            return new LevelUnitSpawner(unitSpawner, unitSpawnerBaseActions);
        }

        private UnitSpawnerAdaptor[] GenerateUnitSpawnerAdaptors(UnitSpawner unitSpawner, UnitWaveInformation unitWaveAdaptor, float spawnRateForPerUnitSpawnPoint, Dictionary<UnitSpawnDetail, UnitSpawnerAdaptor> placedUnitSpawnDetails)
        {
            var unitSpawnerAdaptors = new List<UnitSpawnerAdaptor>();

            foreach (var unitSpawnDetail in unitWaveAdaptor.UnitSpawnDetails)
            {
                float progressToSpawn = unitWaveAdaptor.ProgressToSpawn;

                float delay = unitWaveAdaptor.Delay;
                float timeToSpawn = unitWaveAdaptor.TimeToSpawn * spawnRateForPerUnitSpawnPoint;

                int amount = Mathf.FloorToInt(unitSpawnDetail.Amount * spawnRateForPerUnitSpawnPoint);
                int spawnableAmount = _totalUnitAmount[unitSpawnDetail] - _placedUnitAmount[unitSpawnDetail];
                if (spawnableAmount < amount) amount = spawnableAmount;

                var unitSpawnerAdaptor = new UnitSpawnerAdaptor(unitSpawner, unitSpawnDetail.UnitPrefab, delay, timeToSpawn, amount, progressToSpawn);
                unitSpawnerAdaptors.Add(unitSpawnerAdaptor);

                placedUnitSpawnDetails.Add(unitSpawnDetail, unitSpawnerAdaptor);

                _placedUnitAmount[unitSpawnDetail] += amount;
            }

            return unitSpawnerAdaptors.ToArray();
        }

        private UnitSpawnerAction[] GenerateUnitSpawnerBaseActions(UnitSpawner unitSpawner, UnitSpawnerAdaptor[] unitSpawnerAdaptors)
        {
            var unitSpawnerActions = new List<UnitSpawnerAction>();
            foreach (var unitSpawnerAdaptor in unitSpawnerAdaptors)
            {
                var unitSpawnerAction = unitSpawnerAdaptor.Create(_timer, _progress);
                unitSpawnerActions.Add(unitSpawnerAction);
            }

            return unitSpawnerActions.ToArray();
        }

        private void UnitSpawnAmountControl(LevelUnitSpawner[] levelUnitSpawners, UnitSpawnDetail[] unitSpawnDetails)
        {
            foreach (var unitSpawnDetail in unitSpawnDetails)
            {
                int notSpawnedAmount = _totalUnitAmount[unitSpawnDetail] - _placedUnitAmount[unitSpawnDetail];
                if (notSpawnedAmount < 0)
                {
                    Debug.LogError("TOTAL Unit AMOUNT < PLACED Unit AMOUNT ????? : " + notSpawnedAmount);
                }
                else if (notSpawnedAmount > 0)
                {
                    while (notSpawnedAmount > 0)
                    {
                        int randomLevelUnitSpawnerIndex = Random.Range(0, levelUnitSpawners.Length);
                        var randomSelectedLevelUnitSpawner = levelUnitSpawners[randomLevelUnitSpawnerIndex];
                        _placedLevelUnitSpawners[randomSelectedLevelUnitSpawner][unitSpawnDetail].Amount++;

                        notSpawnedAmount--;
                    }
                }
            }
        }
    }
}
