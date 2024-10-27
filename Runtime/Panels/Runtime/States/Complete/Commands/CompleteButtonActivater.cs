using Boshphelm.Commands; 
using UnityEngine;

namespace Boshphelm.Panel
{
    public class CompleteButtonActivater : Command
    { 
        [SerializeField] private GameObject _rewardButtonGO;  

        public override void StartCommand()
        {  
            _rewardButtonGO.SetActive(true);
            CompleteCommand();
        }
        public override void ResetCommand()
        {
            _rewardButtonGO.SetActive(false);
        }
    }
}
