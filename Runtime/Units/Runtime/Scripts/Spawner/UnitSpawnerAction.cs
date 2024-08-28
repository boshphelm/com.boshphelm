using UnityEngine;

namespace Boshphelm.Units
{
    public abstract class UnitSpawnerAction
    {
        protected readonly GameObject unitPrefab;
        protected readonly UnitSpawner unitSpawner;
        protected readonly int amount;
        protected bool done;
        protected int spawnedUnitCount;

        public int Amount => amount;
        public bool IsDone => done;
        protected bool HasSpawnerReachedToTotalSpawnAmount => spawnedUnitCount >= amount;

        public System.Action<UnitSpawnerAction> OnDone = _ => { };
        public System.Action<UnitSpawnerAction, Unit> OnUnitSpawn = (_, _) => { };

        protected UnitSpawnerAction(GameObject unitPrefab, UnitSpawner unitSpawner, int amount)
        {
            this.unitPrefab = unitPrefab;
            this.unitSpawner = unitSpawner;
            this.amount = amount;
        }

        public abstract void Tick();

        public virtual void SpawnUnit()
        {
            var unit = unitSpawner.Spawn(unitPrefab);

            spawnedUnitCount++;
            OnUnitSpawn.Invoke(this, unit);
        }

        protected virtual void Done()
        {
            if (done) return;

            OnDone.Invoke(this);
            done = true;
        }
    }
}
