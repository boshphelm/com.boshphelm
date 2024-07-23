using System.Collections.Generic;
using Boshphelm.Save;
using UnityEngine;

namespace Boshphelm.Stats
{
    public abstract class StatEffectContainer<T> : MonoBehaviour, ISaveable where T : StatType
    {
        public abstract IStatContainer<T> StatContainer { get; }

        protected List<StatEffectSO<T>> activeEffects = new List<StatEffectSO<T>>();
        protected StatEffectSO<T>[] reloadedEffects;

        private void Start()
        {
            if (reloadedEffects != null && reloadedEffects.Length >= 0) AddMultipleEffects(reloadedEffects);
        }

        public void AddMultipleEffects(StatEffectSO<T>[] statEffectsSO)
        {
            for (int i = 0; i < statEffectsSO.Length; i++) AddEffect(statEffectsSO[i]);
        }

        public void AddEffect(StatEffectSO<T> statEffectSO)
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

        public void RemoveEffect(StatEffectSO<T> statEffectSO)
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
            for (int i = 0; i < activeEffects.Count; i++) effectIDs.Add(activeEffects[i].ID);
            return effectIDs;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;

            var effectIDs = (List<string>)state;
            reloadedEffects = new StatEffectSO<T>[effectIDs.Count];

            for (int i = 0; i < effectIDs.Count; i++)
            {
                var effectSO = StatEffectSO<T>.GetFromID(effectIDs[i]);
                if (effectSO == null) continue;

                reloadedEffects[i] = effectSO;
            }
        }
    }
}