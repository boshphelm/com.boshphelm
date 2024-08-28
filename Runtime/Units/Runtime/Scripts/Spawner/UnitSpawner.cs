using UnityEngine;

namespace Boshphelm.Units
{
    public abstract class UnitSpawner : MonoBehaviour
    {
        [SerializeField] protected Transform[] spawnPoints;

        protected Transform RandomSpawnPoint => spawnPoints[RandomSpawnPointIndex];
        protected int RandomSpawnPointIndex => Random.Range(0, spawnPoints.Length);


        public System.Action<UnitSpawner, Unit> OnUnitSpawn = (_, _) => { };

        public virtual Unit Spawn(GameObject unitPrefab)
        {
            var newUnit = Instantiate(unitPrefab, RandomSpawnPoint);
            return newUnit.GetComponent<Unit>();
        }

    }
}
