using System;
using System.Collections.Generic;
using boshphelm.Conditions;
// using Boshphelm.Saveable;
using boshphelm.Utility;
using UnityEngine.Serialization;

namespace boshphelm.Tutorial
{
    [Serializable]
    public class TutorialSaveDataListEvent : IEvent
    {
        public TutorialSaveDataList TutorialSaveDataList;

        public TutorialSaveDataListEvent(TutorialSaveDataList tutorialSaveDataList)
        {
            TutorialSaveDataList = tutorialSaveDataList;
        }
    }

    [Serializable]
    public class TutorialSaveDataList //: ISaveable
    {
        public SerializableGuid Id { get; set; }
        public List<TutorialSaveData> TutorialSaveDatas;
    }

    [Serializable]
    public class TutorialSaveData
    {
        public int ConditionsByOrderIndex;
        public List<ConditionSaveData> ConditionSaveDatas;
        public List<ConditionSaveData> ConditionsByOrderSaveDatas;
        public bool IsCompleted;
    }
}