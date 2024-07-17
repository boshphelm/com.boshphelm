using System.Collections.Generic;
using UnityEngine;

namespace boshphelm.Tutorial
{
    public class TutorialsCompletedCondition : TutorialCondition
    {
        [SerializeField] private List<Tutorial> _tutorials;
        public override void Activate()
        {
            base.Activate();

            foreach (Tutorial tutorial in _tutorials)
            {
                tutorial.OnTutorialCompleted += OnTutorialCompleted;
            }
        }

        private void OnTutorialCompleted(Tutorial tutorial)
        {
            if (CheckAllTutorialsCompletedStatus()) Complete();

        }
        private bool CheckAllTutorialsCompletedStatus()
        {
            foreach (Tutorial tutorial in _tutorials)
            {
                if (!tutorial.Completed) return false;
            }

            return true;
        }

        public override void Complete()
        {
            foreach (Tutorial tutorial in _tutorials)
            {
                tutorial.OnTutorialCompleted -= OnTutorialCompleted;
            }

            base.Complete();
        }

    }
}
