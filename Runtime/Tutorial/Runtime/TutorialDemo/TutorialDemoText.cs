using System.Collections;
using UnityEngine;

namespace Boshphelm.Tutorial
{
    public class TutorialDemoText : Tutorial
    {
        [SerializeField] private string _hintText;
        public override void StartTutorial()
        { 
            _tutorialFadeManager.tutorialHintManager.HintText(_hintText);
            _tutorialFadeManager.EnableFade();
            StartCoroutine(DelayComplete());
        }

        private IEnumerator DelayComplete()
        {   
            yield return new WaitForSeconds(3);
            CompleteTutorial(); 
            _tutorialFadeManager.DisableFade();
        }
    }
}
