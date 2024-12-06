using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] protected StatType statType;
        public StatType StatType => statType;
        [SerializeField] protected float baseValue;
        private protected float cachedTotalValue;

        public float TotalValue
        {
            get
            {
                if (_isDirty)
                {
                    cachedTotalValue = CalculateFinalValue();
                    _isDirty = false;
                }

                return cachedTotalValue;
            }
        }

        private bool _isDirty = true;
        private List<StatModifier> _modifiers = new List<StatModifier>();

        private List<IStatListener> _valueChangeListeners = new List<IStatListener>();

        public Stat(float baseValue, StatType statType)
        {
            this.baseValue = baseValue;
            this.statType = statType;
            _modifiers.Clear();
            _valueChangeListeners.Clear();
            _isDirty = true;
        }

        public void UpdateBaseValue(float newBaseValue)
        {
            baseValue = newBaseValue;
            _isDirty = true;
            // BroadcastBaseValue();
            BroadcastTotalValue();
        }

        public void RegisterValueListener(IStatListener statListener)
        {
            if (_valueChangeListeners.Contains(statListener)) return;
            _valueChangeListeners.Add(statListener);

            statListener.OnTotalValueChanged(statType, TotalValue);
            //statListener.OnBaseValueChanged(statType, _baseValue);
        }

        public void UnregisterValueListener(IStatListener statListener)
        {
            if (!_valueChangeListeners.Contains(statListener)) return;
            _valueChangeListeners.Remove(statListener);
        }

        /*private void BroadcastBaseValue()
        {
            foreach (IStatListener statListener in _valueChangeListeners)
            {
                statListener.OnBaseValueChanged(statType, _baseValue);
            }
        }*/

        private void BroadcastTotalValue()
        {
            foreach (var statListener in _valueChangeListeners)
            {
                statListener.OnTotalValueChanged(statType, TotalValue);
            }
        }


        public void RemoveSourceModifiers(object source)
        {
            for (int i = _modifiers.Count - 1; i >= 0; i--)
            {
                if (_modifiers[i].Source == source) RemoveModifier(_modifiers[i]);
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            _isDirty = true;
            _modifiers.Add(modifier);
            _modifiers.Sort(CompareModifierOrder);
            BroadcastTotalValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            bool removed = _modifiers.Remove(modifier);
            if (!removed) return;

            _isDirty = true;
            BroadcastTotalValue();
        }

        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }

            if (a.Order > b.Order)
            {
                return 1;
            }

            return 0; // a.order == b.order
        }

        private float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float percentageSum = 0;

            foreach (var statModifier in _modifiers)
            {
                var modType = statModifier.StatModifierType;
                if (modType == StatModifierType.Flat)
                {
                    finalValue += statModifier.Value;
                }
                else if (modType == StatModifierType.PercentageAdd)
                {
                    percentageSum += statModifier.Value;
                }
                else if (modType == StatModifierType.PercentageMultiplier)
                {
                    finalValue *= 1 + statModifier.Value;
                }
            }

            finalValue *= 1 + percentageSum / 100f;


            return finalValue;
        }
    }
}
