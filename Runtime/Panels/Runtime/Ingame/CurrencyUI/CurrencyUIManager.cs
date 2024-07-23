using Boshphelm.Currencies;
using Boshphelm.Wallets;
using UnityEngine;
namespace Boshphelm.Panel
{
    public class CurrencyUIManager : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private CurrencyUI[] _currencyUI;
        public void Init()
        {
            foreach (CurrencyUI currencyUI in _currencyUI)
            {
                Currency currency = _wallet.GetCurrencyByDetail(currencyUI.CurrencyData.detail); 
                if(currency != null) currencyUI.SetAmount(currency.quantity);
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

        private void UpdateCurrencyView(CurrencyDetail currencyDetail, int amount)
        {
            foreach (CurrencyUI currencyUI in _currencyUI)
            {
                if(currencyUI.CurrencyData.Id != currencyDetail.Id) continue;
                currencyUI.SetAmount(amount);
            }
        }
    }
}
