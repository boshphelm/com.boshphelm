using System.Collections.Generic;
using Boshphelm.Currencies;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public class FloatingWalletUIManager : WalletUIManager
    {
        [SerializeField] private FloatingCurrencyNotificationManager _notificationManager;

        private readonly Dictionary<CurrencyDataSO, int> _lastKnownCurrencyValues = new Dictionary<CurrencyDataSO, int>();

        public override void Initialize()
        {
            foreach (var currencyUI in currencyUIs)
            {
                var currency = wallet.GetCurrency(currencyUI.CurrencyData);
                UpdateUIElement(currencyUI, currency);
                _lastKnownCurrencyValues[currencyUI.CurrencyData] = currency.quantity;
            }
        }

        protected override void HandleCurrencyChange(CurrencyDataSO currencyData, Currency currency)
        {
            UpdateUI(currencyData, currency);

            _lastKnownCurrencyValues.TryGetValue(currencyData, out int lastKnownValue);

            int difference = currency.quantity - lastKnownValue;
            if (difference > 0)
            {
                _notificationManager.ShowFloatingCurrencyNotification(currencyData, difference);
            }

            _lastKnownCurrencyValues[currencyData] = currency.quantity;
        }

        private void UpdateUI(CurrencyDataSO currencyData, Currency currency)
        {
            var currencyUI = currencyUIs.Find(element => element.CurrencyData.Id == currencyData.Id);
            if (currencyUI == null) return;

            UpdateUIElement(currencyUI, currency);
        }

        private void UpdateUIElement(CurrencyUI currencyUI, Currency currency)
        {
            if (currencyUI.CurrencyAmountText == null) return;

            currencyUI.CurrencyAmountText.text = currency.quantity.ToString("N0");
        }
    }
}
