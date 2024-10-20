using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boshphelm.GameEvents;
using Boshphelm.Save;
using Boshphelm.Utility;
using Boshphelm.Wallets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Boshphelm.Missions
{
    public class MissionManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private List<MissionType> _allMissionTypes;
        [SerializeField] private GameEventManagerBase _gameEventManagerBase;

        private readonly Dictionary<SerializableGuid, IMission> _allMissions = new Dictionary<SerializableGuid, IMission>();

        private bool _isInitialized;
        private bool _isRestored;

        public event Action OnMissionsUpdated = () => { };
        public event Action<IMission> OnMissionCompleted = _ => { };
        public event Action<IMission> OnMissionRewardCollected = _ => { };
        public event Action<IMission> OnMissionFinished = _ => { };
        public event Action<IMission, float> OnMissionProgressUpdated = (_, _) => { };

        public void Initialize()
        {
            if (_isInitialized) return;

            InitializeMissions(_isRestored);
        }

        private void InitializeMissions(bool isRestoring)
        {
            foreach (var missionType in _allMissionTypes)
            {
                var mission = missionType.CreateMission();
                _allMissions[mission.MissionId] = mission;
                _gameEventManagerBase.AddObserver(mission);
                SubscribeToMissionEvents(mission);
            }

            if (!isRestoring)
            {
                UpdateAllMissions();
            }

            _isInitialized = true;
        }

        private void SubscribeToMissionEvents(IMission mission)
        {
            mission.OnMissionCompleted += HandleMissionCompleted;
            mission.OnRewardCollected += HandleRewardCollected;
            mission.OnProgressUpdated += HandleMissionProgressUpdated;
            mission.OnFinished += HandleMissionFinished;
        }

        private void UpdateAllMissions()
        {
            foreach (var mission in _allMissions.Values)
            {
                if (mission.IsFinished) continue;
                if (mission.IsRewardCollected)
                {
                    mission.Finish();
                    continue;
                }

                //Debug.Log("CAN ACTIVATE MISSION : " + mission.MissionName + ", RESULT : " + CanActivateMission(mission));
                if (!mission.IsActive && CanActivateMission(mission))
                {
                    mission.Activate();
                }
            }
        }

        private bool CanActivateMission(IMission mission)
        {
            foreach (var requiredMissionId in mission.RequiredMissions)
            {
                if (!_allMissions.TryGetValue(requiredMissionId, out var requiredMission)) continue;
                if (requiredMission.IsFinished) continue;

                return false;
            }
            return true;
        }

        private void HandleMissionCompleted(IMission mission)
        {
            OnMissionCompleted?.Invoke(mission);
            OnMissionsUpdated?.Invoke();
        }

        private void HandleRewardCollected(IMission mission)
        {
            OnMissionRewardCollected?.Invoke(mission);
            OnMissionsUpdated?.Invoke();
        }

        private void HandleMissionProgressUpdated(float progress)
        {
            var mission = _allMissions.Values.FirstOrDefault(m => m.Progress == progress);
            if (mission != null)
            {
                OnMissionProgressUpdated?.Invoke(mission, progress);
                OnMissionsUpdated?.Invoke();
            }
        }

        private void HandleMissionFinished(IMission mission)
        {
            UpdateAllMissions();
            OnMissionFinished.Invoke(mission);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var activeMissions = GetActiveMissions();
                int randomIndex = Random.Range(0, activeMissions.Count);
                activeMissions[randomIndex].Complete();
            }
        }

        public void CollectReward(IMission mission)
        {
            if (mission.IsCompleted && !mission.IsRewardCollected)
            {
                mission.CollectReward();
                foreach (var price in mission.Reward)
                {
                    _wallet.AddCurrency(price); 
                }
            }
        }

        public List<IMission> GetAllMissions() => _allMissions.Values.ToList();

        public List<IMission> GetActiveMissions()
        {
            return _allMissions.Values.Where(m => m.IsActive).ToList();
        }

        public List<IMission> GetNextMissions()
        {
            var nextMissions = new List<IMission>();
            var activeMissions = GetActiveMissions();

            foreach (var (id, mission) in _allMissions)
            {
                if (!mission.IsActive) continue;

                foreach (var requiredMissionId in mission.RequiredMissions)
                {
                    if (!_allMissions.TryGetValue(requiredMissionId, out var requiredMission)) continue;
                    if (!nextMissions.Contains(requiredMission)) continue;
                    if (activeMissions.Contains(requiredMission)) continue;

                    nextMissions.Add(requiredMission);
                }

            }

            return nextMissions;
        }

        public object CaptureState()
        {
            var saveData = new MissionManagerSaveData
            {
                AllMissions = _allMissions.Values.Select(mission => new MissionSaveData
                {
                    Id = mission.MissionId,
                    IsActive = mission.IsActive,
                    IsCompleted = mission.IsCompleted,
                    IsFinished = mission.IsFinished,
                    IsRewardCollected = mission.IsRewardCollected,
                    Progress = mission.SaveMission()
                }).ToList()
            };

            return saveData;
        }

        public void RestoreState(object state)
        {
            //Debug.Log("RESTORINGGG MISSSIONSS_0");
            if (state is MissionManagerSaveData saveData)
            {
                //Debug.Log("RESTORED MISSIONS");
                _isRestored = true;

                Initialize();

                foreach (var missionSaveData in saveData.AllMissions)
                {
                    if (_allMissions.TryGetValue(missionSaveData.Id, out var mission))
                    {
                        //Debug.Log("LOADING MISSION : " + mission.MissionName);
                        mission.LoadMission(missionSaveData);
                    }
                }

                UpdateAllMissions();
                OnMissionsUpdated?.Invoke();
            }
        }

        private void OnDestroy()
        {
            foreach (var mission in _allMissions.Values)
            {
                UnsubscribeFromMissionEvents(mission);
            }
        }

        private void UnsubscribeFromMissionEvents(IMission mission)
        {
            mission.OnMissionCompleted -= HandleMissionCompleted;
            mission.OnRewardCollected -= HandleRewardCollected;
            mission.OnProgressUpdated -= HandleMissionProgressUpdated;
            mission.OnFinished -= HandleMissionFinished;
        }
    }
}
