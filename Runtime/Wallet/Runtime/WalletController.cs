using System;
using System.Collections;
using System.Collections.Generic;
using boshphelm.Currencies;
using UnityEngine;

namespace boshphelm.Wallets
{
    public class WalletController
    {
        private readonly WalletModel _model;
        private readonly int _capacity;

        private WalletController(WalletModel model, int capacity)
        {
            Debug.Assert(model != null, "MODEL IS NULL");
            Debug.Assert(capacity > 0, "Capacity is less than 1");

            _model = model;
            _capacity = capacity;

            // TODO: view.StartCoroutine(Initialize())
        }

        public void Bind(WalletData data) => _model.Bind(data);


        private IEnumerator Initialize()
        {
            // TODO: yield return view.InitializeView(capacity);
            yield return null; // PLACEHOLDER FOR UPPER

            // TODO: Subscribe to view events.
            // TODO: Handle view Drop.
            // TODO: Subscribe for model events.
            _model.OnModelChanged += HandleModelChanged;

            RefreshView();
        }

        private void HandleModelChanged(IList<Currency> currencies) => RefreshView();

        private void RefreshView()
        {
            for (int i = 0; i < _capacity; i++)
            {
                Currency currency = _model.Get(i);
                if (currency == null)
                {
                    // TODO: View Slot Set (SerializableGuid.Empty, null);
                }
                else
                {
                    // TODO: View Slot Set currency.id, currency.details.icon, currency.quantity
                }
            }
        }

        public bool CanPayThePrice(Price price) => _model.CanPayThePrice(price);
        public void Pay(Price price) => _model.Pay(price);
        public void Add(Price price) => _model.Add(price);
        public void Add(Currency currency) => _model.Add(currency);
        public Currency GetCurrency(CurrencyDetail currencyDetails) => _model.GetCurrencyByDetail(currencyDetails);


        #region Builder

        public class Builder
        {
            private CurrencyData[] _currencyDetails;
            private int _capacity;
            private Action<CurrencyDetail, int> _onCurrencyUpdated;

            public Builder WithStartingCurrencies(CurrencyData[] currencyDetails)
            {
                _currencyDetails = currencyDetails;
                return this;
            }

            public Builder WithCapacity(int capacity)
            {
                _capacity = capacity;
                return this;
            }

            public Builder WithListeningCurrencyUpdate(Action<CurrencyDetail, int> onCurrencyUpdated)
            {
                _onCurrencyUpdated = onCurrencyUpdated;
                return this;
            }

            public WalletController Build()
            { 
                WalletModel model = new WalletModel(_currencyDetails, _capacity, _onCurrencyUpdated);

                return new WalletController(model, _capacity);
            }
        }

        #endregion
    }
}