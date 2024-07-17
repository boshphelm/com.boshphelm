using boshphelm.Commands;
using UnityEngine;

namespace boshphelm.Tutorial
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