using Boshphelm.InitialLoad;
using UnityEngine;

namespace Boshphelm.DailyRewards
{
    public class DailyInitiateCommand : LoadCommand
    {
        private float _percentageComplete = 0;
        public override float PercentageComplete => _percentageComplete;
        [SerializeField] private DailyRewardsManager _dailyRewardsManager;
        [SerializeField] private DailyRewardsUIController dailyRewardsUIController;
        public override void ResetCommand()
        {
            _percentageComplete = 0;
        }

        public override void StartCommand()
        {
            _dailyRewardsManager.Setup(); 
            dailyRewardsUIController.Init();
            _percentageComplete = 1;
            CompleteCommand();
        }
    }
}
