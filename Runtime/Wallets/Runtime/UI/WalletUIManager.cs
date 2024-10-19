using System.Collections.Generic;
using Boshphelm.Currencies;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public abstract class WalletUIManager : MonoBehaviour
    {
        [SerializeField] protected Wallet wallet;
        [SerializeField] protected List<CurrencyUI> currencyUIs;

        protected virtual void OnEnable()
        {
            wallet.OnCurrencyChanged += HandleCurrencyChange;
        }
        protected virtual void OnDisable()
        {
            wallet.OnCurrencyChanged -= HandleCurrencyChange;
        }

        public abstract void Initialize();
        protected abstract void HandleCurrencyChange(CurrencyDataSO currencyData, Currency currency);
    }
}
