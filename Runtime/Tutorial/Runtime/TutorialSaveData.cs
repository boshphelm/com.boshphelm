using System;
using System.Collections.Generic;
using Boshphelm.Conditions; 
using Boshphelm.Utility; 

namespace Boshphelm.Tutorial
{  
    [Serializable]
    public class TutorialSaveDataList
    {
        public SerializableGuid Id { get; set; }
        public List<TutorialSaveData> TutorialSaveDatas = new List<TutorialSaveData>();
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