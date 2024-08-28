using System;
using System.Collections.Generic;
using System.Linq;
using Boshphelm.Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Boshphelm.Levels
{
    public class LevelUnitTracer
    {
        private readonly LevelUnitSpawner[] _levelUnitSpawners;
        private readonly List<Unit> _units;
        private readonly Dictionary<Unit, LevelUnitSpawner> _unitLevelUnitSpawners;

        public List<Unit> Units => _units;

        public int TotalUnitAmount => _levelUnitSpawners.Sum(levelUnitSpawner => levelUnitSpawner.TotalUnitAmount);

        public Action<LevelUnitSpawner, Unit> onUnitDead = (_, _) => { };
        public Action<LevelUnitSpawner, Unit> onUnitSpawn = (_, _) => { };


        public LevelUnitTracer(LevelUnitSpawner[] levelUnitSpawners)
        {
            _unitLevelUnitSpawners = new Dictionary<Unit, LevelUnitSpawner>();
            _levelUnitSpawners = levelUnitSpawners;
            ListenUnitSpawners();

            _units = new List<Unit>();
        }

        private void ListenUnitSpawners()
        {
            foreach (var levelUnitSpawner in _levelUnitSpawners)
            {
                levelUnitSpawner.OnUnitSpawn += OnUnitSpawn;
            }
        }

        private void OnUnitSpawn(LevelUnitSpawner levelUnitSpawner, Unit unit)
        {
            _unitLevelUnitSpawners.Add(unit, levelUnitSpawner);
            _units.Add(unit);

            unit.OnDead += OnUnitDead;

            onUnitSpawn.Invoke(levelUnitSpawner, unit);
        }


        private void OnUnitDead(Unit unit, GameObject killer)
        {
            unit.OnDead -= OnUnitDead;

            onUnitDead.Invoke(_unitLevelUnitSpawners[unit], unit);
            _unitLevelUnitSpawners.Remove(unit);
        }

        public void Reset()
        {
            foreach (var levelUnitSpawner in _levelUnitSpawners)
            {
                levelUnitSpawner.OnUnitSpawn -= OnUnitSpawn;
            }

            for (int i = _units.Count - 1; i >= 0; i--)
            {
                if (_units[i] == null || _units[i].transform == null) continue;

                Object.Destroy(_units[i].gameObject);
            }

            _units.Clear();
            onUnitDead = (_, _) => { };
            onUnitSpawn = (_, _) => { };
        }
    }
}
