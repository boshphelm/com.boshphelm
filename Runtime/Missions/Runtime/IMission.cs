using System;
using System.Collections;
using System.Collections.Generic;
using Boshphelm.Currencies;
using Boshphelm.GameEvents;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Missions
{
    public interface IMission : IObserver
    {
        SerializableGuid MissionId { get; }
        string MissionName { get; }
        string Description { get; }
        bool IsCompleted { get; }
        bool IsActive { get; }
        bool IsFinished { get; }
        bool IsRewardCollected { get; }
        List<SerializableGuid> RequiredMissions { get; }
        float Progress { get; }
        List<Price> Reward { get; }

        event Action<float> OnProgressUpdated;
        event Action<IMission> OnMissionCompleted;
        event Action<IMission> OnRewardCollected;
        event Action<IMission> OnFinished;

        void Activate();
        void Complete();
        bool CheckCompletion();
        void CollectReward();
        void Finish();
        object SaveMission();
        void LoadMission(MissionSaveData missionSaveData);
        void LoadProgress(object progress);
    }

    [Serializable]
    public class MissionManagerSaveData
    {
        public List<MissionSaveData> AllMissions;
    }

    [Serializable]
    public class MissionSaveData
    {
        public SerializableGuid Id;
        public bool IsActive;
        public bool IsCompleted;
        public bool IsFinished;
        public bool IsRewardCollected;
        public object Progress;
    }
}
