using System.Collections.Generic;
using boshphelm.Save; 
using UnityEngine;

namespace boshphelm.Tutorial
{
    public class TutorialManager : MonoBehaviour , ISaveable
    { 
        private TutorialSaveDataList _tutorialSaveDataList; 

        [SerializeField] private List<Tutorial> _tutorials; 
        public void Init()
        {
            TriggerNotCompletedTutorials();
        }    

        public object CaptureState()
        {
            return _tutorialSaveDataList;
        }

        public void RestoreState(object state)
        {   
            if(state == null) _tutorialSaveDataList = new TutorialSaveDataList(); 
            else _tutorialSaveDataList = (TutorialSaveDataList)state; 

            for (int i = 0; i < _tutorials.Count; i++)
            {
                Tutorial tutorial = _tutorials[i];  
                if (_tutorialSaveDataList.TutorialSaveDatas.Count <= i)
                    _tutorialSaveDataList.TutorialSaveDatas.Add(new TutorialSaveData());
                tutorial.Bind(_tutorialSaveDataList.TutorialSaveDatas[i]);
            }  
        }

        private void TriggerNotCompletedTutorials()
        {
            foreach (Tutorial tutorial in _tutorials)
            {
                if (tutorial.Completed) continue;

                tutorial.ListenAllConditionsAndStartTutorial();
            }
        }
    }
}