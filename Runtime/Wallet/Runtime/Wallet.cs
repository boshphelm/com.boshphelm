using System;
using System.Collections.Generic;
using boshphelm.Currencies;
using boshphelm.Save;
using boshphelm.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace boshphelm.Wallets
{
    public class Wallet : MonoBehaviour , ISaveable
    {  
        [SerializeField] private CurrencyData[] _startingCurrencies;

        private WalletController _controller; 
        private WalletData _walletData = new WalletData();

        public Action<CurrencyDetail, int> OnCurrencyChanged;  

        public object CaptureState()
        { 
            return _walletData;
        }

        public void RestoreState(object state)
        { 
            if(state != null) _walletData = (WalletData)state;   
        }

        public void Init()
        { 
            _controller = new WalletController.Builder()
                .WithCapacity(_walletData.Capacity)
                .WithStartingCurrencies(_startingCurrencies)
                .WithListeningCurrencyUpdate(OnCurrencyUpdated)
                .Build();  
                
            Bind(_walletData);      
        } 
 
        private void OnCurrencyUpdated(CurrencyDetail currencyDetail, int quantity)
        {
            OnCurrencyChanged?.Invoke(currencyDetail, quantity);
            Debug.Log("CHANGED CURRENCY : " + currencyDetail + ", NEW QUANTITY : " + quantity);
        }  

        public void Bind(WalletData data)
        {
            _controller.Bind(data);
        } 

        public bool CanPayThePrice(Price price) => _controller.CanPayThePrice(price);
        public void Pay(Price price) => _controller.Pay(price);
        public Currency GetCurrencyByDetail(CurrencyDetail currencyDetails) => _controller.GetCurrency(currencyDetails);
        public void Add(Currency currency) => _controller.Add(currency);
        public void Add(Price price) => _controller.Add(price);
    }

    [Serializable]
    public class WalletData
    { 
        public Currency[] Currencies;
        public int Capacity = 1;
    }  
}