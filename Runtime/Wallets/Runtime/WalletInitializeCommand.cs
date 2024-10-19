using Boshphelm.Commands;
using UnityEngine;

namespace Boshphelm.Wallets
{
    public class WalletInitializeCommand : Command
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private WalletUIManager _walletUIManager;
        public override void StartCommand()
        {
            _wallet.Initialize();
            if (_walletUIManager != null) _walletUIManager.Initialize();

            CompleteCommand();
        }
        public override void ResetCommand()
        {
        }
    }
}
