using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Boshphelm.Missions
{
    public class MissionRewardCollectionNotifier : MonoBehaviour
    {
        [SerializeField] private MissionManager _missionManager;
        [SerializeField] private GameObject _notificationIcon;
        [SerializeField] private Button _openPanelButton;
        [SerializeField] private Button _closePanelButton;
        [SerializeField] private GameObject _missionPanel;

        private int _uncollectedMissionRewardsCount;

        public void Initialize()
        {
            _missionManager.OnMissionCompleted += HandleMissionCompleted;
            _missionManager.OnMissionRewardCollected += HandleMissionRewardCollected;

            _openPanelButton.onClick.AddListener(OpenMissionPanel);
            _closePanelButton.onClick.AddListener(CloseMissionPanel);

            InitializeUncollectedMissionRewardsCount();
            UpdateNotificationVisibility();
        }
        private void CloseMissionPanel()
        {
            _missionPanel.SetActive(false);
        }

        private void InitializeUncollectedMissionRewardsCount()
        {
            _uncollectedMissionRewardsCount = 0;
            foreach (var mission in _missionManager.GetAllMissions())
            {
                if (mission.IsCompleted && !mission.IsRewardCollected)
                {
                    _uncollectedMissionRewardsCount++;
                }
            }
        }

        private void HandleMissionCompleted(IMission mission)
        {
            _uncollectedMissionRewardsCount++;
            UpdateNotificationVisibility();
        }

        private void HandleMissionRewardCollected(IMission mission)
        {
            _uncollectedMissionRewardsCount = Mathf.Clamp(_uncollectedMissionRewardsCount - 1, 0, int.MaxValue);
            UpdateNotificationVisibility();
        }

        private void UpdateNotificationVisibility()
        {
            if (_notificationIcon != null)
            {
                _notificationIcon.SetActive(_uncollectedMissionRewardsCount > 0);
            }
        }

        private void OpenMissionPanel()
        {
            _uncollectedMissionRewardsCount = _missionManager.GetUncollectedMissionRewardCount();
            UpdateNotificationVisibility();

            _missionPanel.SetActive(true);
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
        }
    }
}
