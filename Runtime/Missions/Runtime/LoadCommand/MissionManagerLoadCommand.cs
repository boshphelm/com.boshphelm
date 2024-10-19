using Boshphelm.InitialLoad;
using UnityEngine;

namespace Boshphelm.Missions
{
    public class MissionManagerLoadCommand : LoadCommand
    {
        [SerializeField] private MissionManager _missionManager;
        [SerializeField] private MissionUIManager _missionUIManager;

        private float _percentageComplete;
        public override float PercentageComplete => _percentageComplete;

        public override void StartCommand()
        {
            _percentageComplete = 0f;
            _missionManager.Initialize();
            _missionUIManager.Initialize();
            _percentageComplete = 1f;
            CompleteCommand();
        }
        public override void ResetCommand()
        {

        }
    }
}
