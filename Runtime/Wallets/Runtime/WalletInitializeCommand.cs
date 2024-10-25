using Boshphelm.Commands;
using Boshphelm.InitialLoad;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public class WalletInitializeCommand : LoadCommand
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private WalletUIManager _walletUIManager;
        private float _percentageComplete;

        public override float PercentageComplete => _percentageComplete;

        public override void StartCommand()
        {
            _percentageComplete = 0;
            _wallet.Initialize();
            if (_walletUIManager != null) _walletUIManager.Initialize();

            CompleteCommand();
            _percentageComplete = 1;
        }
        public override void ResetCommand()
        {
        }
    }
}
