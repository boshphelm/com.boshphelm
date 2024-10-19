using Boshphelm.Currencies;

namespace Boshphelm.Wallets
{
    public class VirtualWallet : Wallet
    {
        public override void Initialize()
        {
        }

        public void StartLevel(Price initialCurrency)
        {
            Refresh();

            AddCurrency(initialCurrency.CurrencyDetails, initialCurrency.Amount);
        }

        public void EndLevel()
        {
        }

        private void Refresh()
        {
            earnedCurrencies.Clear();
        }
    }
}
