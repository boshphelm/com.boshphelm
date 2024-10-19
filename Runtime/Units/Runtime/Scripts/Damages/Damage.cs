namespace Boshphelm.Units
{
    public class Damage : IDamage
    {
        public bool Resistable { get; }
        public float Amount { get; private set; }
        public DamageType Type { get; private set; }
        public Unit Source { get; private set; }

        public Damage(float amount, DamageType type, Unit source, bool resistable = true)
        {
            Amount = amount;
            Type = type;
            Source = source;
            Resistable = resistable;
        }
    }
}
