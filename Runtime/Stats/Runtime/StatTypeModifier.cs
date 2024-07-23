namespace Boshphelm.Stats
{
    [System.Serializable]
    public class StatTypeModifier<T> where T : StatType
    {
        public T StatType;
        public StatModifier Modifier;
    }
}