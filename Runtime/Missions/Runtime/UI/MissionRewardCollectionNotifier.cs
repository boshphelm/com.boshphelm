using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Missions
{
    public class MissionRewardCollectionNotifier : MonoBehaviour
    {
        [SerializeField] private MissionManager _missionManager;
        [SerializeField] private MissionCompletedPopUpNotifier _missionCompletedPopUpNotifier;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _notificationIcon;
        [SerializeField] private GameObject _mainMissionsNotificationIcon;
        [SerializeField] private GameObject _sideMissionsNotificationIcon;
        [SerializeField] private Button _openPanelButton;
        [SerializeField] private Button _closePanelButton;
        [SerializeField] private GameObject _missionPanel;

        private int _uncollectedMainMissionRewardsCount;
        private int _uncollectedSideMissionRewardsCount;
        private int _uncollectedTotalRewardsCount => _uncollectedMainMissionRewardsCount + _uncollectedSideMissionRewardsCount;

        public void Initialize()
        {
            _missionManager.OnMissionCompleted += HandleMissionCompleted;
            _missionManager.OnMissionRewardCollected += HandleMissionRewardCollected;

            _openPanelButton.onClick.AddListener(OpenMissionPanel);
            _closePanelButton.onClick.AddListener(CloseMissionPanel);

            InitializeUncollectedMissionRewardsCount();
            UpdateAllNotificationVisibility();
        }

        private void CloseMissionPanel()
        {
            _missionPanel.SetActive(false);
        }

        private void InitializeUncollectedMissionRewardsCount()
        {
            _uncollectedMainMissionRewardsCount = 0;
            _uncollectedSideMissionRewardsCount = 0;

            foreach (var mission in _missionManager.GetAllMissions())
            {
                if (mission.IsCompleted && !mission.IsRewardCollected)
                {
                    if (mission.Main)
                    {
                        _uncollectedMainMissionRewardsCount++;
                    }
                    else
                    {
                        _uncollectedSideMissionRewardsCount++;
                    }
                }
            }
        }

        private void HandleMissionCompleted(IMission mission)
        {
            if (mission.Main)
            {
                _uncollectedMainMissionRewardsCount++;
            }
            else
            {
                _uncollectedSideMissionRewardsCount++;
            }
            UpdateAllNotificationVisibility();
            _missionCompletedPopUpNotifier.Show();
        }

        private void HandleMissionRewardCollected(IMission mission)
        {
            if (mission.Main)
            {
                _uncollectedMainMissionRewardsCount = Mathf.Clamp(_uncollectedMainMissionRewardsCount - 1, 0, int.MaxValue);
            }
            else
            {
                _uncollectedSideMissionRewardsCount = Mathf.Clamp(_uncollectedSideMissionRewardsCount - 1, 0, int.MaxValue);
            }
            UpdateAllNotificationVisibility();
        }

        private void UpdateAllNotificationVisibility()
        { 
            // Global notification icon
            if (_notificationIcon != null)
            {
                _notificationIcon.SetActive(_uncollectedTotalRewardsCount > 0);
            }

            // Main missions notification icon
            if (_mainMissionsNotificationIcon != null)
            {
                _mainMissionsNotificationIcon.SetActive(_uncollectedMainMissionRewardsCount > 0);
            }

            // Side missions notification icon
            if (_sideMissionsNotificationIcon != null)
            {
                _sideMissionsNotificationIcon.SetActive(_uncollectedSideMissionRewardsCount > 0);
            }
        }

        private void OpenMissionPanel()
        {
            RefreshUncollectedRewardCounts();
            UpdateAllNotificationVisibility();
            _missionPanel.SetActive(true);
            UpdateScrollRectPositionToTop();
        }
        private void UpdateScrollRectPositionToTop()
        {
            Canvas.ForceUpdateCanvases();
            _scrollRect.normalizedPosition = new Vector2(0, 1);
        }
        private void RefreshUncollectedRewardCounts()
        {
            _uncollectedMainMissionRewardsCount = 0;
            _uncollectedSideMissionRewardsCount = 0;

            foreach (var mission in _missionManager.GetAllMissions())
            {
                if (mission.IsCompleted && !mission.IsRewardCollected)
                {
                    if (mission.Main)
                        _uncollectedMainMissionRewardsCount++;
                    else
                        _uncollectedSideMissionRewardsCount++;
                }
            }
        }

        private void OnDestroy()
        {
            if (_missionManager != null)
            {
                _missionManager.OnMissionCompleted -= HandleMissionCompleted;
                _missionManager.OnMissionRewardCollected -= HandleMissionRewardCollected;
            }

            if (_openPanelButton != null)
            {
                _openPanelButton.onClick.RemoveListener(OpenMissionPanel);
            }

            if (_closePanelButton != null)
            {
                _closePanelButton.onClick.RemoveListener(CloseMissionPanel);
            }
        }
    }
}