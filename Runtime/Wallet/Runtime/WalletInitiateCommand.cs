using boshphelm.Commands;
using UnityEngine;

namespace boshphelm.Wallets
{
    [RequireComponent(typeof(Wallet))]
    public class WalletInitiateCommand : Command
    { 
        [SerializeField] private Wallet _wallet;
        public override void StartCommand()
        { 
            _wallet.Init();

            CompleteCommand();
        }
        public override void ResetCommand()
        { 
        }
    }
}