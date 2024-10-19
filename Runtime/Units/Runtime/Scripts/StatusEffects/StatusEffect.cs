using System.Collections.Generic;
using Boshphelm.Stats;

namespace Boshphelm.Units
{
    public abstract class StatusEffect
    {
        public StatusEffectData Data { get; }
        public Unit Source { get; }
        public int Level { get; }
        public float Duration { get; }

        protected Dictionary<Unit, List<StatModifier>> AppliedModifiers = new Dictionary<Unit, List<StatModifier>>();

        protected StatusEffect(StatusEffectData data, Unit source, int level, float duration)
        {
            Data = data;
            Source = source;
            Level = level;
            Duration = duration;
        }

        public virtual void ApplyEffect(Unit target)
        {
            var levelData = GetCurrentLevelData();
            foreach (var statTypeStatModifier in levelData.StatTypeStatModifiers)
            {
                var stat = target.UnitStatContainer.GetStatByStatType(statTypeStatModifier.StatType);
                if (stat == null) continue;

                var modifier = new StatModifier(statTypeStatModifier.StatModifier, this);
                stat.AddModifier(modifier);
                AddModifier(target, modifier);
            }
        }
        public virtual void RemoveEffect(Unit target)
        {
            target.UnitStatContainer.RemoveModifiersFromSource(this);

            AppliedModifiers.Remove(target);
        }

        public virtual void UpdateEffect(Unit target, float deltaTime) { }

        public StatusEffectLevelData GetCurrentLevelData() => Data.GetLevelData(Level);

        protected void AddModifier(Unit target, StatModifier modifier)
        {
            if (!AppliedModifiers.TryGetValue(target, out var modifiers))
            {
                modifiers = new List<StatModifier>();
                AppliedModifiers[target] = modifiers;
            }
            modifiers.Add(modifier);
        }

        protected void RemoveModifier(Unit target, StatModifier modifier)
        {
            if (AppliedModifiers.TryGetValue(target, out var modifiers))
            {
                modifiers.Remove(modifier);
                if (modifiers.Count == 0)
                {
                    AppliedModifiers.Remove(target);
                }
            }
        }

        public void RemoveEffectFromAllTargets()
        {
            foreach (var target in new List<Unit>(AppliedModifiers.Keys))
            {
                RemoveEffect(target);
            }
            AppliedModifiers.Clear();
        }

        public abstract void Refresh();
    }
}
