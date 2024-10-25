using System.Collections.Generic;
using System.Linq;
using Boshphelm.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Missions
{
    public class MissionUIManager : MonoBehaviour
    {
        [SerializeField] private AssetKits.ParticleImage.ParticleImage _moneyParticle;
        [SerializeField] private MissionManager _missionManager;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _missionCardPrefab;
        [SerializeField] private Transform _missionCardContainer;
        [SerializeField] private Button _mainMissionsButton;
        [SerializeField] private Button _sideMissionsButton;
        private readonly Dictionary<SerializableGuid, MissionCard> _missionCards = new Dictionary<SerializableGuid, MissionCard>();
        private bool _showingMainMissions = true; 
        public void Initialize()
        {
            InitializeMissionCards();
            SubscribeToEvents();
            SetupMissionTypeButtons();

            var missionRewardCollectionNotifier = GetComponent<MissionRewardCollectionNotifier>();
            missionRewardCollectionNotifier.Initialize();

            UpdateScrollRectPositionToTop();
        }

        private void SubscribeToEvents()
        {
            if (_missionManager == null) return;

            _missionManager.OnMissionsUpdated += UpdateMissionCards;
            _missionManager.OnMissionCompleted += HandleMissionCompleted;
            _missionManager.OnMissionRewardCollected += HandleMissionRewardCollected;
            _missionManager.OnMissionProgressUpdated += HandleMissionProgressUpdated;
            _missionManager.OnMissionFinished += HandleMissionFinished;
        }

        private void SetupMissionTypeButtons()
        {
            _mainMissionsButton.onClick.AddListener(() =>
            {
                _showingMainMissions = true;
                ShowMissionsByType(true);
                MissionTabsButtonStates();
                UpdateScrollRectPositionToTop();
            });

            _sideMissionsButton.onClick.AddListener(() =>
            {
                _showingMainMissions = false;
                ShowMissionsByType(false);
                MissionTabsButtonStates();
                UpdateScrollRectPositionToTop();
            });

            ShowMissionsByType(true);
            MissionTabsButtonStates();
        }
        private void UpdateScrollRectPositionToTop()
        {
            Canvas.ForceUpdateCanvases();
            _scrollRect.normalizedPosition = new Vector2(0, 1);
        }
        private void MissionTabsButtonStates()
        {
            _mainMissionsButton.interactable = !_showingMainMissions;
            _sideMissionsButton.interactable = _showingMainMissions;
        }

        private void ShowMissionsByType(bool showMainMissions)
        {
            var activeMissions = _missionManager.GetActiveMissions();
            var nextMissions = _missionManager.GetNextMissions();
            foreach (var missionCard in _missionCards.Values)
            {
                bool isActive = activeMissions.Contains(missionCard.Mission);
                bool isNext = nextMissions.Contains(missionCard.Mission);

                bool shouldShow = missionCard.Mission.Main == showMainMissions;

                missionCard.UpdateCardStatus(shouldShow, isActive, isNext);
            }

            SortMissionCards();
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

        private void CreateMissionCard(IMission mission)
        {
            var cardObject = Instantiate(_missionCardPrefab, _missionCardContainer);
            var missionCard = cardObject.GetComponent<MissionCard>();

            if (missionCard != null)
            {
                missionCard.SetupCard(mission, mission.IsActive, false, CollectReward);
                _missionCards[mission.MissionId] = missionCard;

                cardObject.SetActive(mission.Main == _showingMainMissions);
            }
        }

        private void UpdateMissionCards()
        {
            var activeMissions = _missionManager.GetActiveMissions();
            var nextMissions = _missionManager.GetNextMissions();

            foreach (var missionCard in _missionCards.Values)
            {
                var mission = missionCard.Mission;
                if (mission.Main == _showingMainMissions)
                {
                    bool isActive = activeMissions.Contains(mission);
                    bool isNext = nextMissions.Contains(mission);
                    missionCard.RefreshCard(isActive, isNext);
                }
            }

            SortMissionCards();
        }

        private void SortMissionCards()
        {
            var sortedCards = new List<MissionCard>();
            var currentTypeMissions = new List<IMission>();

            foreach (var card in _missionCards.Values)
            {
                if (card.Mission.Main == _showingMainMissions)
                {
                    currentTypeMissions.Add(card.Mission);
                }
            }

            var activeMissions = _missionManager.GetActiveMissions().OrderByDescending(m => m.IsCompleted && !m.IsRewardCollected).ToList();
            foreach (var mission in activeMissions)
            {
                if (mission.Main == _showingMainMissions && _missionCards.TryGetValue(mission.MissionId, out var card))
                {
                    sortedCards.Add(card);
                }
            }

            foreach (var mission in _missionManager.GetNextMissions())
            {
                if (mission.Main == _showingMainMissions && _missionCards.TryGetValue(mission.MissionId, out var card))
                {
                    if (!sortedCards.Contains(card))
                    {
                        sortedCards.Add(card);
                    }
                }
            }

            foreach (var mission in currentTypeMissions)
            {
                if (_missionCards.TryGetValue(mission.MissionId, out var card))
                {
                    if (!sortedCards.Contains(card))
                    {
                        sortedCards.Add(card);
                    }
                }
            }

            for (int i = 0; i < sortedCards.Count; i++)
            {
                sortedCards[i].transform.SetSiblingIndex(i);
            }
        }

        private void CollectReward(IMission mission)
        {
            if (_missionCards.TryGetValue(mission.MissionId, out var card)) SetupParticle(card.GetComponent<RectTransform>().position);
            _missionManager.CollectReward(mission);
        }

        private void SetupParticle(Vector3 originPosition)
        {
            _moneyParticle.gameObject.SetActive(false);
            _moneyParticle.rectTransform.position = originPosition;
            _moneyParticle.gameObject.SetActive(true);
        }

        private void HandleMissionCompleted(IMission mission)
        {
            if (_missionCards.TryGetValue(mission.MissionId, out var card) && mission.Main == _showingMainMissions)
            {
                card.RefreshCard(mission.IsActive, false);
            }
        }

        private void HandleMissionRewardCollected(IMission mission)
        {
            if (_missionCards.TryGetValue(mission.MissionId, out var card) && mission.Main == _showingMainMissions)
            {
                card.RefreshCard(mission.IsActive, false);
                mission.Finish();
            }
        }

        private void HandleMissionProgressUpdated(IMission mission, float progress)
        {
            if (_missionCards.TryGetValue(mission.MissionId, out var card) && mission.Main == _showingMainMissions)
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
            if (_missionManager != null)
            {
                _missionManager.OnMissionsUpdated -= UpdateMissionCards;
                _missionManager.OnMissionCompleted -= HandleMissionCompleted;
                _missionManager.OnMissionRewardCollected -= HandleMissionRewardCollected;
                _missionManager.OnMissionProgressUpdated -= HandleMissionProgressUpdated;
                _missionManager.OnMissionFinished -= HandleMissionFinished;
            }

            _mainMissionsButton.onClick.RemoveAllListeners();
            _sideMissionsButton.onClick.RemoveAllListeners();
        }
    }
}
