using Boshphelm.InitialLoad;
using UnityEngine;

namespace Boshphelm.Tutorial
{
    public class TutorialInitiateCommand : LoadCommand
    {
        [SerializeField] private TutorialManager _tutorialManager;
        private float _percent = 0;

        public override float PercentageComplete => _percent;

        public override void StartCommand()
        {
            _tutorialManager.Init();
            CompleteCommand();
            _percent = 1;
        }

        public override void ResetCommand()
        {
        }
    }
}