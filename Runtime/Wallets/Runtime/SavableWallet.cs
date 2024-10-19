using System.Collections.Generic;
using Boshphelm.Currencies;
using Boshphelm.Save;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public class SavableWallet : Wallet, ISaveable
    {
        [SerializeField] private Price[] _initialCurrencies;

        private bool _isRestored;

        public override void Initialize()
        {
            if (!_isRestored)
            {
                GenerateStartingCurrencies();
            }
        }

        private void GenerateStartingCurrencies()
        {
            earnedCurrencies.Clear();

            foreach (var startInitialCurrency in _initialCurrencies)
            {
                AddCurrency(startInitialCurrency.CurrencyDetails, startInitialCurrency.Amount);
            }
        }

        public object CaptureState()
        {
            var savedCurrencies = new Dictionary<SerializableGuid, int>();
            foreach (var kvp in earnedCurrencies)
            {
                savedCurrencies[kvp.Key] = kvp.Value.quantity;
            }
            return savedCurrencies;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;

            var savedCurrencies = (Dictionary<SerializableGuid, int>)state;
            earnedCurrencies.Clear();

            foreach (var kvp in savedCurrencies)
            {
                var currencyData = CurrencyDatabase.GetCurrencyDataById(kvp.Key); //Resources.Load<CurrencyDataSO>("Currencies/" + kvp.Key);
                if (currencyData != null)
                {
                    earnedCurrencies[kvp.Key] = currencyData.Create(kvp.Value);
                }
            }

            _isRestored = true;
        }
    }
}
