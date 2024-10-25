
using System.Collections;
using UnityEngine;

namespace Boshphelm.Tutorial
{
    public class TutorialDemoCondition : TutorialCondition
    {   
        public override void Activate()
        {
            if (Completed) return;
            StartCoroutine(DelayComplete());
        }

        private IEnumerator DelayComplete()
        {
            yield return new WaitForSeconds(1);
            Complete();
        }
    }
}
