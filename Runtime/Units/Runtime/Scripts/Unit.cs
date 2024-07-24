using Boshphelm.Teams;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected UnitHealth unitHealth;
        [SerializeField] protected UnitStatContainer unitStatContainer;
        [SerializeField] private TeamType _teamType;

        public TeamType TeamType => _teamType;

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

        protected virtual void TakeDamage(float damage, GameObject attacker)
        {
            unitHealth.TakeDamage(damage, attacker);
            OnTakeDamage.Invoke(this, damage);
        }

        protected virtual void Heal(float heal, GameObject healer)
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
    }
}
