using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Units
{
    [System.Serializable]
    public class UnitWaveInformation
    {
        public UnitSpawnDetail[] UnitSpawnDetails;
        [Range(1, 10)] public int SpawnerCount;

        [MinValue(0)] public float Delay;
        [MinValue(0)] public float TimeToSpawn;
        [Space]
        [Range(0, 1)] public float ProgressToSpawn;
    }


    [System.Serializable]
    public class UnitSpawnDetail
    {
        public GameObject UnitPrefab;
        public int Amount = 1;
    }

    public class UnitSpawnerAdaptor
    {
        public UnitSpawner UnitSpawner;
        public GameObject UnitPrefab;

        public float Delay;
        public float TotalTimeToSpawn;
        public int Amount;

        public float TargetProgress;

        public UnitSpawnerAdaptor(UnitSpawner unitSpawner, GameObject unitPrefab, float delay, float totalTimeToSpawn, int amount, float targetProgress)
        {
            UnitSpawner = unitSpawner;
            UnitPrefab = unitPrefab;
            Delay = delay;
            TotalTimeToSpawn = totalTimeToSpawn;
            Amount = amount;
            TargetProgress = targetProgress;
        }

        public UnitSpawnerAction Create(ITimer timer, IProgress progress) => new SpawnerActionByTimeOrProgress(UnitPrefab, UnitSpawner, Amount, timer, progress, TotalTimeToSpawn, TargetProgress, Delay);
    }
}
