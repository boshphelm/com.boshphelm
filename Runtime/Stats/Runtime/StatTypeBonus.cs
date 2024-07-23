namespace Boshphelm.Stats
{
    public class StatTypeBonus<T> where T : StatType
    {
        public T StatType;
        public StatModifierType ModifierType;
        public float Value;

        public StatTypeBonus(StatTypeModifier<T> statTypeModifier)
        {
            ModifierType = statTypeModifier.Modifier.StatModifierType;
            StatType = statTypeModifier.StatType;
            Value = statTypeModifier.Modifier.Value;
        }

        public StatTypeBonus(T statType, float value = 0f, StatModifierType modifierType = StatModifierType.Flat)
        {
            StatType = statType;
            Value = value;
            ModifierType = modifierType;
        }
    }
}