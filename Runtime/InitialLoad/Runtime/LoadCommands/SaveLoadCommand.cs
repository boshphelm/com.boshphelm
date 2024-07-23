using Boshphelm.Save;
using UnityEngine;

namespace Boshphelm.InitialLoad
{
    public class SaveLoadCommand : LoadCommand
    {
        [SerializeField] private SavingWrapper _savingWrapper;

        public override float PercentageComplete => _percentageComplete;
        private float _percentageComplete;

        public override void StartCommand()
        {
            _percentageComplete = 0f;
            _savingWrapper.Load();
            _percentageComplete = 1f;

            CompleteCommand();
        }

        public override void ResetCommand()
        {
        }
    }
}