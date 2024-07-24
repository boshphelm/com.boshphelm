using Boshphelm.Stats;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Units
{
    [RequireComponent(typeof(UnitStatContainer))]
    public class UnitHealth : Health, IStatListener
    {
        [SerializeField] private UnitStatType _totalHealthStatType;

        public void Initialize()
        {
            var unitStatContainer = GetComponent<UnitStatContainer>();
            var healthStat = unitStatContainer.GetStatByStatType(_totalHealthStatType);
            healthStat.RegisterValueListener(this);
        }

        public void OnTotalValueChanged(StatType statType, float newTotalHealth)
        {
            if (statType.Id != _totalHealthStatType.Id) return;

            UpdateMaxHealth(newTotalHealth);
        }
    }
}
