using System;
using Boshphelm.Currencies;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public class WalletModel
    {
        public ObservableArray<Currency> Currencies { get; set; }

        public Action<CurrencyDetail, int> OnCurrencyUpdated = (_, _) => { };

        private WalletData _walletData;
        private readonly int _capacity;

        public event Action<Currency[]> OnModelChanged
        {
            add => Currencies.AnyValueChanged += value;
            remove => Currencies.AnyValueChanged -= value;
        }

        public WalletModel(CurrencyData[] currencyDetails, int capacity, Action<CurrencyDetail, int> onCurrencyUpdated)
        {
            _capacity = capacity;
            Currencies = new ObservableArray<Currency>(_capacity);
            foreach (CurrencyData currencyDetail in currencyDetails)
            {
                Currencies.TryAdd(currencyDetail.Create(0));
            }

            OnCurrencyUpdated += onCurrencyUpdated;
        }

        public void Bind(WalletData data)
        {
            _walletData = data;
            _walletData.Capacity = _capacity;

            bool isNew = _walletData.Currencies == null || _walletData.Currencies.Length == 0;

            if (isNew)
            {
                _walletData.Currencies = new Currency[_capacity];
            }
            else
            {
                for (int i = 0; i < _capacity; i++)
                {
                    if (Currencies[i] == null) continue;
                    _walletData.Currencies[i].detail = CurrencyDatabase.GetDetailsById(Currencies[i].detailsId);
                }
            }

            if (isNew && Currencies.Count != 0)
            {
                for (int i = 0; i < _capacity; i++)
                {
                    if (Currencies[i] == null) continue;
                    _walletData.Currencies[i] = Currencies[i];
                }
            }

            Currencies.items = _walletData.Currencies;
        }

        public Currency Get(int index) => Currencies[index];
        public void Clear() => Currencies.Clear();

        public void Add(Price price)
        {
            Currency currency = GetCurrencyByDetail(price.CurrencyDetails.detail);
            if (currency == null)
            {
                Add(currency);
            }
            else
            { 
                currency.quantity += price.Amount; 
                OnCurrencyUpdated.Invoke(currency.detail, currency.quantity);
            }
        }

        public void Add(Currency currency)
        { 
            Currency foundCurrency = GetCurrencyByDetail(currency.detail);
            if (foundCurrency == null)
            {
                if (Currencies.TryAdd(currency))
                {
                    foundCurrency = Currencies[^1];
                }
                else
                {
                    throw new Exception("CURRENCY CANNOT ADD IN WALLET : " + currency.detail + ", CURRENCY COUNT : " + Currencies.Count);
                }
            }
            else
            {
                foundCurrency.quantity += currency.quantity;
            }

            OnCurrencyUpdated.Invoke(foundCurrency.detail, foundCurrency.quantity);
        }

        public void Remove(Currency currency)
        {
            Currency foundCurrency = GetCurrencyByDetail(currency.detail);
            if (foundCurrency == null) return;

            foundCurrency.quantity -= currency.quantity;
            OnCurrencyUpdated.Invoke(foundCurrency.detail, foundCurrency.quantity);
        }

        public bool CanPayThePrice(Price price)
        {
            Currency currency = GetCurrencyByDetail(price.CurrencyDetails.detail);
            if (currency == null) return false;

            bool hasEnoughCurrencyAmount = currency.quantity >= price.Amount;
            return hasEnoughCurrencyAmount;
        }

        public Currency GetCurrencyByDetail(CurrencyDetail currencyDetails) => GetCurrencyByDetailId(currencyDetails.Id);

        public Currency GetCurrencyByDetailId(SerializableGuid currencyDetailsId)
        {
            for (int i = 0; i < Currencies.Count; i++)
            {
                if (Currencies[i].detailsId != currencyDetailsId) continue;
                return Currencies[i];
            }

            return null;
        }

        public void Pay(Price price)
        {
            Currency currency = GetCurrencyByDetail(price.CurrencyDetails.detail);
            if (currency == null) return;

            currency.quantity = Mathf.Clamp(currency.quantity - price.Amount, 0, int.MaxValue);
            Currencies.Invoke();

            OnCurrencyUpdated.Invoke(currency.detail, currency.quantity);
        }

        public void Swap(int source, int target) => Currencies.Swap(source, target);

        public int Combine(int source, int target)
        {
            int total = Currencies[source].quantity + Currencies[target].quantity;
            Currencies[target].quantity = total;
            Remove(Currencies[source]);
            return total;
        }
    }
}