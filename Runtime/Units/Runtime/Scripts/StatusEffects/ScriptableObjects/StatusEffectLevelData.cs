using System.Collections.Generic;
using Boshphelm.Stats;

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
        public StatType StatType;
        public StatModifier StatModifier;
    }
}
