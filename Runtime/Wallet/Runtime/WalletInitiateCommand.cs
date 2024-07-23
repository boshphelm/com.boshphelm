using Boshphelm.Commands;
using UnityEngine;

namespace Boshphelm.Wallet
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