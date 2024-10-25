 
using UnityEngine;

namespace Boshphelm.Tutorial
{
    public class TutorialDemoText : Tutorial
    {
        [SerializeField] private string _hintText;
        public override void StartTutorial()
        { 
            _tutorialFadeManager.tutorialHintManager.HintText(_hintText);
        }
    }
}
