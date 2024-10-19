using System.Collections.Generic;
using Boshphelm.Currencies;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public abstract class Wallet : MonoBehaviour
    {
        protected readonly Dictionary<SerializableGuid, Currency> earnedCurrencies = new Dictionary<SerializableGuid, Currency>();

        public System.Action<CurrencyDataSO, Currency> OnCurrencyChanged = (_, _) => { };

        public abstract void Initialize();

        public void AddCurrency(Price price) => AddCurrency(price.CurrencyDetails, price.Amount);

        public virtual void AddCurrency(CurrencyDataSO currencyData, int quantity)
        {
            earnedCurrencies.TryAdd(currencyData.Id, currencyData.Create(0));

            var currency = earnedCurrencies[currencyData.Id];
            currency.quantity += quantity;

            OnCurrencyChanged.Invoke(currencyData, currency);
        }

        public virtual bool RemoveCurrency(CurrencyDataSO currencyData, int quantity)
        {
            if (HaveEnoughCurrency(currencyData, quantity))
            {
                earnedCurrencies[currencyData.Id].quantity -= quantity;
                OnCurrencyChanged.Invoke(currencyData, earnedCurrencies[currencyData.Id]);
                return true;
            }
            return false;
        }

        public bool HaveEnoughCurrency(CurrencyDataSO currencyData, int quantity) => earnedCurrencies.TryGetValue(currencyData.Id, out var currency) && currency.quantity >= quantity;
        public Currency GetCurrency(CurrencyDataSO currencyData) => earnedCurrencies.TryGetValue(currencyData.Id, out var currency) ? currency : currencyData.Create(0);
    }
}
