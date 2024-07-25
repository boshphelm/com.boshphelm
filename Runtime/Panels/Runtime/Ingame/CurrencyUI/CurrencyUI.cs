using Boshphelm.Currencies; 
using TMPro;
using UnityEngine;
using Boshphelm.Utility;
namespace Boshphelm.Panel
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private CurrencyDataSO _currencyData;
        public CurrencyDataSO CurrencyData { get => _currencyData; }
        [SerializeField] private TextMeshProUGUI _currencyAmountText;
        private int _currentAmount = 0;
        public void SetAmount(int amount)
        {
            _currencyAmountText.ValueIncreaser(amount, _currentAmount, 1, DG.Tweening.Ease.OutQuad);
            _currentAmount = amount;
        }
    }
}
