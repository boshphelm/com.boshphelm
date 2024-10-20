using System.Collections.Generic;
using Boshphelm.Units;

namespace DiceTowers.Utility
{
    public abstract class StatusEffectAuraApplier
    {
        private readonly UnitDetector _unitDetector;
        private readonly Unit _owner;
        private readonly List<Unit> _affectedUnits = new List<Unit>();

        private bool _isActive;
        public bool IsActive => _isActive;

        protected Unit Owner => _owner;

        public StatusEffectAuraApplier(Unit owner, UnitDetector unitDetector)
        {
            _owner = owner;
            _unitDetector = unitDetector;
        }

        public virtual void Activate()
        {
            if (_isActive) return;

            _isActive = true;
            _unitDetector.OnUnitDetected += ApplyAuraToUnit;
            _unitDetector.OnUnitRemoved += RemoveAuraFromUnit;
        }

        public virtual void Deactivate()
        {
            if (!_isActive) return;

            _isActive = false;
            _unitDetector.OnUnitDetected -= ApplyAuraToUnit;
            _unitDetector.OnUnitRemoved -= RemoveAuraFromUnit;

            for (int i = _affectedUnits.Count - 1; i >= 0; i--)
            {
                RemoveAuraFromUnit(_affectedUnits[i]);
            }
        }

        public virtual void Reset()
        {
            Deactivate();
            _affectedUnits.Clear();
        }

        private void ApplyAuraToUnit(Unit unit)
        {
            if (unit == _owner || unit.TeamType != _owner.TeamType) return;

            ApplyStatusEffectToUnit(unit);
            _affectedUnits.Add(unit);
        }

        private void RemoveAuraFromUnit(Unit unit)
        {
            if (!_affectedUnits.Contains(unit)) return;

            RemoveStatusEffectFromUnit(unit);
            _affectedUnits.Remove(unit);
        }

        protected abstract void ApplyStatusEffectToUnit(Unit unit);
        protected abstract void RemoveStatusEffectFromUnit(Unit unit);
    }
}
