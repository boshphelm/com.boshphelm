using UnityEngine;

namespace Boshphelm.Utility
{
    public abstract class Health : MonoBehaviour
    {
        protected float maxHealth;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => maxHealth;
        public float HealthPercentage => CurrentHealth / MaxHealth;
        public int HealthPercentage100 => (int)(CurrentHealth / MaxHealth * 100f);

        public bool AmIDead { get; private set; }
        public bool Untouchable { get; set; }
        public bool GodMode { get; set; }
        public bool IsMyHealthFull => CurrentHealth == MaxHealth;

        public System.Action<Health, GameObject> OnDead = (_, _) => { };
        public System.Action<float, float, GameObject> OnTakeDamage = (_, _, _) => { };
        public System.Action<float, float, GameObject> OnHeal = (_, _, _) => { };

        public GameObject LastHitter { get; private set; }

        public void TakeDamageByMaxHealthPercentage(float damagePercentage, GameObject attacker)
        {
            float damageAmount = MaxHealth * damagePercentage / 100f;
            TakeDamage(damageAmount, attacker);
        }
        public void TakeDamageByCurrentHealthPercentage(float damagePercentage, GameObject attacker)
        {
            float damageAmount = CurrentHealth * damagePercentage / 100f;
            TakeDamage(damageAmount, attacker);
        }

        public void TakeDamage(float damageValue, GameObject attacker)
        {
            if (GodMode) return;
            if (Untouchable) return;
            if (AmIDead) return;

            LastHitter = attacker;

            if (CurrentHealth <= damageValue)
            {
                CurrentHealth = 0;
                OnDead.Invoke(this, LastHitter);
            }
            else
            {
                CurrentHealth -= damageValue;
            }

            OnTakeDamage.Invoke(CurrentHealth, damageValue, attacker);
        }

        public void Heal(float healAmount, GameObject source)
        {
            if (CurrentHealth + healAmount >= MaxHealth)
            {
                healAmount = MaxHealth - CurrentHealth;
            }

            CurrentHealth += healAmount;

            OnHeal.Invoke(CurrentHealth, healAmount, source);
        }

        public void HealByPercentage(float healPercentage, GameObject source)
        {
            float healingAmount = MaxHealth * healPercentage / 100f;
            Heal(healingAmount, source);
        }

        public void Revive(float percentageOfHealth)
        {
            if (!AmIDead) return;

            AmIDead = false;
            float resurrectionHealth = MaxHealth * percentageOfHealth / 100f;
            CurrentHealth = resurrectionHealth;
        }

        public void UpdateMaxHealth(float newMaxHealth)
        {
            float healthPercentageBeforeMaxHealthUpdate = HealthPercentage;
            maxHealth = newMaxHealth;
            if (float.IsNaN(healthPercentageBeforeMaxHealthUpdate))
            {
                CurrentHealth = maxHealth;
            }
            else
            {
                CurrentHealth = maxHealth * healthPercentageBeforeMaxHealthUpdate;
            }
        }

        public void KillMe(GameObject source)
        {
            TakeDamage(MaxHealth, source);
        }
    }
}
