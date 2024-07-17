using System.Collections.Generic;
// using Boshphelm.Saveable;
// using boshphelm.Utility;
using UnityEngine;

namespace boshphelm.Tutorial
{
    public class TutorialManager : MonoBehaviour //, IBind<TutorialSaveDataList>
    {
        //public SerializableGuid Id { get; set; }

        private TutorialSaveDataList _tutorialSaveDataList;
        //private EventBinding<TutorialSaveDataListEvent> _tutorialSaveDataListEvent;

        [SerializeField] private List<Tutorial> _tutorials;

        public void Init()
        {
            TriggerNotCompletedTutorials();
        }

        private void OnEnable()
        {
            // _tutorialSaveDataListEvent = new EventBinding<TutorialSaveDataListEvent>(HandleBuildingControllerData);
            // EventBus<TutorialSaveDataListEvent>.Register(_tutorialSaveDataListEvent);
        }

        private void OnDisable()
        {
            // EventBus<TutorialSaveDataListEvent>.Deregister(_tutorialSaveDataListEvent);
        }

        private void HandleBuildingControllerData(TutorialSaveDataListEvent tutorialSaveDataListEvent)
        {
            Bind(tutorialSaveDataListEvent.TutorialSaveDataList);
        }

        public void Bind(TutorialSaveDataList tutorialSaveDataList)
        {
            _tutorialSaveDataList = tutorialSaveDataList;
            if (_tutorialSaveDataList.TutorialSaveDatas == null)
            {
                _tutorialSaveDataList.TutorialSaveDatas = new List<TutorialSaveData>();
            }

            for (int i = 0; i < _tutorials.Count; i++)
            {
                Tutorial tutorial = _tutorials[i];

                if (_tutorialSaveDataList.TutorialSaveDatas.Count <= i)
                {
                    _tutorialSaveDataList.TutorialSaveDatas.Add(new TutorialSaveData());
                }

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