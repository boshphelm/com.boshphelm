using Boshphelm.Commands;
using UnityEngine;

namespace Boshphelm.Shops
{
    [RequireComponent(typeof(Shop))]
    public class ShopInitiateCommand : Command
    {
        public override void StartCommand()
        {
            var shop = GetComponent<Shop>();
            shop.Init();

            CompleteCommand();
        }
        public override void ResetCommand()
        {             
        }
    }
}