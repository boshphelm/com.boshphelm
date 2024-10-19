namespace Boshphelm.Units
{
    public interface IDamage
    {
        bool Resistable { get; }
        float Amount { get; }
        DamageType Type { get; }
        Unit Source { get; }
    }
}
