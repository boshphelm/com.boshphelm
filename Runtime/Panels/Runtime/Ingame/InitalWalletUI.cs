using System.Collections;
using System.Collections.Generic;
using Boshphelm.Commands;
using UnityEngine;

namespace Boshphelm.Panel
{
    public class InitalWalletUI : Command
    {
        [SerializeField] private CurrencyUIManager _currencyUIManager;

        public override void StartCommand()
        {
            _currencyUIManager.Init();
            CompleteCommand();
        }
        public override void ResetCommand()
        {             
        }
    }
}
