using TMPro;
using UnityEngine;
using Boshphelm.Utility;

namespace Boshphelm.Currencies
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private CurrencyDataSO _currencyData;
        [SerializeField] private TextMeshProUGUI _currencyAmountText;

        public CurrencyDataSO CurrencyData => _currencyData;
        private int _currentAmount;

        public void SetAmount(int amount)
        {
            _currencyAmountText.ValueIncreaser(amount, _currentAmount, 1, DG.Tweening.Ease.OutQuad);
            _currentAmount = amount;
        }
    }
}
