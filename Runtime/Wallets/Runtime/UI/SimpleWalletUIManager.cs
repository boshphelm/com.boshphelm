using Boshphelm.Currencies;

namespace Boshphelm.Wallets
{
    public class SimpleWalletUIManager : WalletUIManager
    {
        public override void Initialize()
        {
            foreach (var currencyUI in currencyUIs)
            {
                var currency = wallet.GetCurrency(currencyUI.CurrencyData);
                if (currency != null) currencyUI.SetAmount(currency.quantity);
            }
        }

        protected override void HandleCurrencyChange(CurrencyDataSO currencyData, Currency currency)
        {
            foreach (var currencyUI in currencyUIs)
            {
                if (currencyUI.CurrencyData.Id != currencyData.Id) continue;
                currencyUI.SetAmount(currency.quantity);
            }
        }
    }
}
