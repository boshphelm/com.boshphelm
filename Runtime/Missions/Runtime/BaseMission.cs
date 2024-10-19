using System.Collections.Generic;
using Boshphelm.Currencies;
using Boshphelm.GameEvents;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Missions
{
    public abstract class BaseMission : IMission
    {
        public SerializableGuid MissionId { get; protected set; }
        public string MissionName { get; protected set; }
        public string Description { get; protected set; }
        public bool IsCompleted { get; protected set; }
        public bool IsActive { get; protected set; }
        public bool IsFinished { get; protected set; }
        public bool IsRewardCollected { get; protected set; }
        public List<SerializableGuid> RequiredMissions { get; protected set; }
        public float Progress { get; protected set; }
        public List<Price> Reward { get; }

        public event System.Action<float> OnProgressUpdated = _ => { };
        public event System.Action<IMission> OnMissionCompleted = _ => { };
        public event System.Action<IMission> OnRewardCollected = _ => { };
        public event System.Action<IMission> OnFinished = _ => { };

        protected BaseMission(MissionInfo info)
        {
            MissionId = info.Id;
            MissionName = info.Name;
            Description = info.Description;
            Reward = info.Reward;
            RequiredMissions = info.RequiredMissions;
            IsCompleted = false;
            IsActive = false;
            IsFinished = false;
            IsRewardCollected = false;
        }

        public virtual void Activate()
        {
            //Debug.Log("ACTIVATING MISSION_2");
            if (!IsFinished)
            {
                // Debug.Log("MISSION ACTIVATING : " + MissionName);
                IsActive = true;
            }
        }

        public virtual void Complete()
        {
            if (IsFinished || IsRewardCollected) return;

            //Debug.Log("COMPLETE : " + MissionName);
            IsCompleted = true;
            OnMissionCompleted.Invoke(this);
        }

        public virtual void CollectReward()
        {
            if (IsCompleted && !IsRewardCollected)
            {
                IsRewardCollected = true;
                OnRewardCollected?.Invoke(this);
            }
        }

        public virtual void Finish()
        {
            if (!IsRewardCollected) return;

            IsActive = false;
            IsCompleted = true;
            IsRewardCollected = true;
            IsFinished = true;
            OnFinished.Invoke(this);
        }

        protected void UpdateProgress(float newProgress)
        {
            if (Progress == newProgress) return;

            Progress = newProgress;
            OnProgressUpdated?.Invoke(Progress);
            if (CheckCompletion())
            {
                Complete();
            }
        }

        public virtual void LoadMission(MissionSaveData saveData)
        {
            IsActive = saveData.IsActive;
            IsCompleted = saveData.IsCompleted;
            IsRewardCollected = saveData.IsRewardCollected;
            IsFinished = saveData.IsFinished;

            if (!IsFinished) LoadProgress(saveData.Progress);
        }

        public abstract bool CheckCompletion();
        public abstract object SaveMission();
        public abstract void LoadProgress(object savedData);
        public abstract void OnNotify(IGameEvent gameEvent);
    }
}
