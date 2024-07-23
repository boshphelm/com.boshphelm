using System.Collections.Generic;
using System.Linq;

namespace Boshphelm.Stats
{
    public class StatTypeComparer<T> where T : StatType
    {
        private readonly List<StatTypeBonus<T>> _first, _second;

        private Dictionary<T, StatTypeBonus<T>> _compareDic;

        public StatTypeComparer(List<StatTypeBonus<T>> first, List<StatTypeBonus<T>> second)
        {
            _first = first;
            _second = second;

            GenerateCompareDic();
        }

        private void GenerateCompareDic()
        {
            _compareDic = new Dictionary<T, StatTypeBonus<T>>();
            AddToCompareDic(_first);
            AddToCompareDic(_second);
        }

        public void AddToCompareDic(List<StatTypeBonus<T>> bonuses)
        {
            if (bonuses == null) bonuses = new List<StatTypeBonus<T>>();

            foreach (var bonus in bonuses)
            {
                if (_compareDic.ContainsKey(bonus.StatType)) continue;

                _compareDic.Add(bonus.StatType, new StatTypeBonus<T>(bonus.StatType, 0f, bonus.ModifierType));
            }
        }

        public List<StatTypeBonus<T>> Compare()
        {
            var compareBonuses = new List<StatTypeBonus<T>>();

            foreach (var item in _compareDic)
            {
                var curFirst = GetStatTypeBonusDataByType(_first, item.Key);
                float firstValue = curFirst == null ? 0f : curFirst.Value;

                var curSecond = GetStatTypeBonusDataByType(_second, item.Key);
                float secondValue = curSecond == null ? 0f : curSecond.Value;

                if (curFirst == null && curSecond == null) continue;

                StatModifierType modifierType = curFirst == null ? curSecond.ModifierType : curFirst.ModifierType;

                compareBonuses.Add(new StatTypeBonus<T>(item.Key, secondValue - firstValue, modifierType));
            }

            return compareBonuses;
        }

        private StatTypeBonus<T> GetStatTypeBonusDataByType(List<StatTypeBonus<T>> statTypeBonuses, T statType)
        {
            return statTypeBonuses.FirstOrDefault(statTypeBonus => statTypeBonus.StatType == statType);
        }
    }
}