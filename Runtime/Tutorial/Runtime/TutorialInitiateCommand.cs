using Boshphelm.Commands;
using UnityEngine;

namespace Boshphelm.Tutorial
{
    public class TutorialInitiateCommand : Command
    {
        [SerializeField] private TutorialManager _tutorialManager;

        public override void StartCommand()
        {
            _tutorialManager.Init();
            CompleteCommand();
        }

        public override void ResetCommand()
        {
        }
    }
}