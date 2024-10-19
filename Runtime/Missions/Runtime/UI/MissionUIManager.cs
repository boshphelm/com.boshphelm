using System.Collections.Generic;
using Boshphelm.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Missions
{
    public class MissionUIManager : MonoBehaviour
    {
        [SerializeField] private MissionManager _missionManager;
        [SerializeField] private GameObject _missionCardPrefab;
        [SerializeField] private Transform _missionCardContainer;
        [SerializeField] private ScrollRect _scrollRect;

        private readonly Dictionary<SerializableGuid, MissionCard> _missionCards = new Dictionary<SerializableGuid, MissionCard>();

        public void Initialize()
        {
            InitializeMissionCards();
            SubscribeToEvents();

            var missionRewardCollectionNotifier = GetComponent<MissionRewardCollectionNotifier>();
            missionRewardCollectionNotifier.Initialize();
        }

        private void InitializeMissionCards()
        {
            var allMissions = _missionManager.GetAllMissions();
            foreach (var mission in allMissions)
            {
                if (mission.IsFinished) continue;

                CreateMissionCard(mission);
            }
            UpdateMissionCards();
        }

        private void SubscribeToEvents()
        {
            _missionManager.OnMissionsUpdated += UpdateMissionCards;
            _missionManager.OnMissionCompleted += HandleMissionCompleted;
            _missionManager.OnMissionRewardCollected += HandleMissionRewardCollected;
            _missionManager.OnMissionProgressUpdated += HandleMissionProgressUpdated;
            _missionManager.OnMissionFinished += HandleMissionFinished;
        }

        private void CreateMissionCard(IMission mission)
        {
            var cardObject = Instantiate(_missionCardPrefab, _missionCardContainer);
            var missionCard = cardObject.GetComponent<MissionCard>();

            if (missionCard != null)
            {
                //Debug.Log("MISSION : " + mission.MissionName + ", CARD CREATING : " + mission.MissionId.ToHexString());
                missionCard.SetupCard(mission, mission.IsActive, false, CollectReward);
                _missionCards[mission.MissionId] = missionCard;
            }
        }

        private void UpdateMissionCards()
        {
            var activeMissions = _missionManager.GetActiveMissions();
            var nextMissions = _missionManager.GetNextMissions();

            foreach (var missionCard in _missionCards.Values)
            {
                bool isActive = activeMissions.Contains(missionCard.Mission);
                bool isNext = nextMissions.Contains(missionCard.Mission);
                missionCard.RefreshCard(isActive, isNext);
            }

            // Optional: Sort cards based on status (active, next, others)
            SortMissionCards();
        }

        private void SortMissionCards()
        {
            var sortedCards = new List<Transform>();

            // First, add active missions
            foreach (var mission in _missionManager.GetActiveMissions())
            {
                if (_missionCards.TryGetValue(mission.MissionId, out var card))
                {
                    sortedCards.Add(card.transform);
                }
            }

            // Then, add next missions
            foreach (var mission in _missionManager.GetNextMissions())
            {
                if (_missionCards.TryGetValue(mission.MissionId, out var card))
                {
                    if (!sortedCards.Contains(card.transform))
                    {
                        sortedCards.Add(card.transform);
                    }
                }
            }

            // Finally, add remaining missions
            foreach (var card in _missionCards.Values)
            {
                if (!sortedCards.Contains(card.transform))
                {
                    sortedCards.Add(card.transform);
                }
            }

            // Set the sorted order
            for (int i = 0; i < sortedCards.Count; i++)
            {
                sortedCards[i].SetSiblingIndex(i);
            }

            // Scroll to top after sorting
            Canvas.ForceUpdateCanvases();
            //_scrollRect.normalizedPosition = new Vector2(0, 1);
        }

        private void CollectReward(IMission mission)
        {
            _missionManager.CollectReward(mission);
        }

        private void HandleMissionCompleted(IMission mission)
        {
            if (_missionCards.TryGetValue(mission.MissionId, out var card))
            {
                card.RefreshCard(mission.IsActive, false);
            }
        }

        private void HandleMissionRewardCollected(IMission mission)
        {
            if (_missionCards.TryGetValue(mission.MissionId, out var card))
            {
                card.RefreshCard(mission.IsActive, false);

                // TODO: Reward Collect UI Animation ?
                mission.Finish();
            }
        }

        private void HandleMissionProgressUpdated(IMission mission, float progress)
        {
            if (_missionCards.TryGetValue(mission.MissionId, out var card))
            {
                card.RefreshCard(mission.IsActive, false);
            }
        }

        private void HandleMissionFinished(IMission mission)
        {
            if (!_missionCards.Remove(mission.MissionId, out var card)) return;

            Destroy(card.gameObject);
        }

        private void OnDestroy()
        {
            // Unsubscribe from events to prevent memory leaks
            if (_missionManager != null)
            {
                _missionManager.OnMissionsUpdated -= UpdateMissionCards;
                _missionManager.OnMissionCompleted -= HandleMissionCompleted;
                _missionManager.OnMissionRewardCollected -= HandleMissionRewardCollected;
                _missionManager.OnMissionProgressUpdated -= HandleMissionProgressUpdated;
                _missionManager.OnMissionFinished -= HandleMissionFinished;
            }
        }
    }
}
