namespace Boshphelm.Units
{
    public class StatusEffectInstance
    {
        public StatusEffect Effect { get; private set; }
        public float? RemainingDuration { get; private set; }
        public Unit Target { get; private set; }

        public StatusEffectInstance(StatusEffect effect, Unit target)
        {
            Effect = effect;
            Target = target;
            RemainingDuration = effect.Data.IsPermanent ? null : effect.Duration;
        }

        public void UpdateDuration(float deltaTime)
        {
            if (RemainingDuration.HasValue)
            {
                RemainingDuration -= deltaTime;
            }
        }

        public bool IsExpired() => RemainingDuration.HasValue && RemainingDuration <= 0;
    }
}
