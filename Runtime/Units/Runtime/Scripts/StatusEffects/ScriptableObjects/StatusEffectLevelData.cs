using System.Collections.Generic;
using Boshphelm.Stats;
using UnityEngine;

namespace Boshphelm.Units
{
    [System.Serializable]
    public class StatusEffectLevelData
    {
        public int Level;
        public float DefaultDuration;
        public List<StatTypeStatModifier> StatTypeStatModifiers;
    }

    [System.Serializable]
    public class StatTypeStatModifier
    {
        public UnitStatType StatType;
        public StatModifier StatModifier;
    }
}
