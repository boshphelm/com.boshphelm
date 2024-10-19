using Boshphelm.Stats;
using Boshphelm.Teams;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected UnitHealth unitHealth;
        [SerializeField] protected UnitStatContainer unitStatContainer;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private TeamType _teamType;
        [SerializeField] private StatusEffectManager _statusEffectManager;
        [SerializeField] private Transform _speedBoostParticlePoint;

        public Transform SpeedBoostParticlePoint => _speedBoostParticlePoint;
        public DamageType DamageType => _damageType;
        public UnitStatContainer UnitStatContainer => unitStatContainer;
        public UnitHealth UnitHealth => unitHealth;
        public TeamType TeamType => _teamType;
        public StatusEffectManager StatusEffectManager => _statusEffectManager;

        public System.Action<Unit, GameObject> OnDead = (_, _) => { };
        public System.Action<Unit, float> OnTakeDamage = (_, _) => { };
        public System.Action<Unit, float> OnHeal = (_, _) => { };
        public System.Action<Unit> OnRevive = _ => { };



        public virtual void Initialize()
        {
            unitStatContainer.Initialize();
            unitHealth.Initialize();

            unitHealth.OnDead += OnImDead;
        }

        protected virtual void OnImDead(Health myHealth, GameObject killer)
        {
            OnDead.Invoke(this, killer);
        }

        public virtual void TakeDamage(IDamage damage)
        {
            float resistance = damage.Resistable ? GetDamageResistance(damage.Type) : 0f;
            float finalDamage = damage.Amount * (1 - resistance);
//            Debug.Log("DAMAGE TYPE : " + damage.Type.ResistanceStatType + ", AMOUNT : " + damage.Amount + ", RESISTANCE : " + resistance + ", FINAL DAMAGE : " + finalDamage, gameObject);
            unitHealth.TakeDamage(finalDamage, damage.Source.gameObject);
            OnTakeDamage.Invoke(this, finalDamage);
        }

        private float GetDamageResistance(DamageType damageType)
        {
            var resistanceStat = unitStatContainer.GetStatByStatType(damageType.ResistanceStatType);
            if (resistanceStat != null)
            {
                return resistanceStat.TotalValue / 100f;
            }
            return 0f;
        }

        public virtual void Heal(float heal, GameObject healer)
        {
            unitHealth.Heal(heal, healer);
            OnHeal.Invoke(this, heal);
        }

        public virtual void Revive()
        {
            if (!unitHealth.AmIDead) return;

            unitHealth.Revive(100f);
            OnRevive.Invoke(this);
        }

        public void SetLevel(int level)
        {
            UnitStatContainer.SetLevel(level);
        }

        public void Suicide()
        {
            if (unitHealth.AmIDead) return;

            var damage = new Damage(unitHealth.CurrentHealth, _damageType, this, false);
            TakeDamage(damage);
        }

        public abstract void Burn(bool burning);
    }
}
