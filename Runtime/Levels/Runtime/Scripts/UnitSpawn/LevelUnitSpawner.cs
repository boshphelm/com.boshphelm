using System.Collections.Generic;
using System.Linq;
using Boshphelm.Units;

namespace Boshphelm.Levels
{
    public class LevelUnitSpawner
    {
        private readonly UnitSpawner _unitSpawner;
        private readonly List<UnitSpawnerAction> _unitSpawnerActionList;

        public int TotalUnitAmount => _unitSpawnerActionList.Sum(unitSpawnerAction => unitSpawnerAction.Amount);

        public System.Action<LevelUnitSpawner, Unit> OnUnitSpawn = (_, _) => { };

        public LevelUnitSpawner(UnitSpawner unitSpawner, UnitSpawnerAction[] unitSpawnerActions)
        {
            _unitSpawner = unitSpawner;
            _unitSpawnerActionList = new List<UnitSpawnerAction>(unitSpawnerActions);

            for (int i = _unitSpawnerActionList.Count - 1; i >= 0; i--)
            {
                if (_unitSpawnerActionList[i].IsDone)
                {
                    _unitSpawnerActionList.RemoveAt(i);
                }
                else
                {
                    _unitSpawnerActionList[i].OnUnitSpawn += OnActionSpawnUnit;
                    _unitSpawnerActionList[i].OnDone += OnUnitSpawnerActionDone;
                }
            }
        }
        private void OnActionSpawnUnit(UnitSpawnerAction unitSpawnerAction, Unit unit)
        {
            OnUnitSpawn.Invoke(this, unit);
        }
        private void OnUnitSpawnerActionDone(UnitSpawnerAction unitSpawnerAction)
        {
            unitSpawnerAction.OnDone -= OnUnitSpawnerActionDone;
            unitSpawnerAction.OnUnitSpawn -= OnActionSpawnUnit;
            _unitSpawnerActionList.Remove(unitSpawnerAction);
        }

        public void Tick()
        {
            foreach (var unitSpawnerAction in _unitSpawnerActionList)
            {
                if (unitSpawnerAction.IsDone) return;

                unitSpawnerAction.Tick();
            }
        }
    }
}
