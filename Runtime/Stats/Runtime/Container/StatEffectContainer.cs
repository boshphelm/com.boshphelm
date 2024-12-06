using System.Collections.Generic;
using Boshphelm.Save;
using UnityEngine;

namespace Boshphelm.Stats
{
    public abstract class StatEffectContainer : MonoBehaviour, ISaveable
    {
        public abstract IStatContainer StatContainer { get; }

        protected List<StatEffectSO> activeEffects = new List<StatEffectSO>();
        protected StatEffectSO[] reloadedEffects;

        private void Start()
        {
            if (reloadedEffects != null) AddMultipleEffects(reloadedEffects);
        }

        public void AddMultipleEffects(StatEffectSO[] statEffectsSO)
        {
            for (int i = 0; i < statEffectsSO.Length; i++)
            {
                AddEffect(statEffectsSO[i]);
            }
        }

        public void AddEffect(StatEffectSO statEffectSO)
        {
            Debug.Log(statEffectSO.StatEffects[0].StatType.ToString() + " type : " + statEffectSO.StatEffects[0].StatModifier.Value);
            for (int i = 0; i < statEffectSO.StatEffects.Length; i++)
            {
                var tankStat = StatContainer.GetStatByStatType(statEffectSO.StatEffects[i].StatType);
                if (tankStat == null) continue;

                tankStat.AddModifier(new StatModifier(statEffectSO.StatEffects[i].StatModifier, statEffectSO));
            }

            activeEffects.Add(statEffectSO);
        }

        public void RemoveEffect(StatEffectSO statEffectSO)
        {
            for (int i = 0; i < statEffectSO.StatEffects.Length; i++)
            {
                var tankStat = StatContainer.GetStatByStatType(statEffectSO.StatEffects[i].StatType);
                if (tankStat == null) continue;

                tankStat.RemoveSourceModifiers(statEffectSO);
            }

            activeEffects.Remove(statEffectSO);
        }


        public object CaptureState()
        {
            var effectIDs = new List<string>();
            for (int i = 0; i < activeEffects.Count; i++)
            {
                effectIDs.Add(activeEffects[i].ID);
            }
            return effectIDs;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;

            var effectIDs = (List<string>)state;
            reloadedEffects = new StatEffectSO[effectIDs.Count];

            for (int i = 0; i < effectIDs.Count; i++)
            {
                var effectSO = StatEffectSO.GetFromID(effectIDs[i]);
                if (effectSO == null) continue;

                reloadedEffects[i] = effectSO;
            }
        }
    }
}
