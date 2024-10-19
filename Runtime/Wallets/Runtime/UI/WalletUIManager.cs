using Boshphelm.Currencies;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public class WalletUIManager : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private CurrencyUI[] _currencyUI;

        public void Initialize()
        {
            foreach (var currencyUI in _currencyUI)
            {
                var currency = _wallet.GetCurrency(currencyUI.CurrencyData);
                if (currency != null) currencyUI.SetAmount(currency.quantity);
            }
        }
        private void OnEnable()
        {
            _wallet.OnCurrencyChanged += UpdateCurrencyView;
        }
        private void OnDisable()
        {
            _wallet.OnCurrencyChanged -= UpdateCurrencyView;
        }

        private void UpdateCurrencyView(CurrencyDataSO currencyDetail, Currency currency)
        {
            foreach (var currencyUI in _currencyUI)
            {
                if (currencyUI.CurrencyData.Id != currencyDetail.Id) continue;
                currencyUI.SetAmount(currency.quantity);
            }
        }
    }
}
