using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Missions
{
    public class MissionCard : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private ProgressFillBar _progressFillBar;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private Button _collectButton;
        [SerializeField] private GameObject _nextMissionLabel;
        [SerializeField] private GameObject _lockedMissionGO;

        private IMission _mission;
        private Action<IMission> _onCollectReward;

        public IMission Mission => _mission;

        public void SetupCard(IMission mission, bool isActive, bool isNext, Action<IMission> onCollectReward)
        {
            _mission = mission;
            _onCollectReward = onCollectReward;

            UpdateCardInfo();
            UpdateCardStatus(isActive, isNext);
            SubscribeToMissionEvents();
        }

        private void UpdateCardInfo()
        {
            _titleText.text = _mission.MissionName;
            _descriptionText.text = _mission.Description;
            _rewardText.text = _mission.Reward[0].Amount.ToString();
            UpdateProgress();
            UpdateCollectButton();
        }

        private void UpdateCardStatus(bool isActive, bool isNext)
        {
            //Debug.Log("IS ACTIVE : " + isActive + ", IS NEXT : " + isNext);
            if (_mission.IsCompleted)
            {
                _lockedMissionGO.SetActive(false);
            }
            else if (isActive)
            {
                _lockedMissionGO.SetActive(false);
            }
            else if (isNext)
            {
                _lockedMissionGO.SetActive(true);
            }

            _nextMissionLabel.SetActive(isNext);
        }

        private void UpdateProgress()
        {
            _progressFillBar.UpdateFillAmount(_mission.Progress);
            _progressText.text = $"{_mission.Progress * 100:F0}%";
        }

        private void UpdateCollectButton()
        {
            bool canCollect = _mission.IsCompleted && !_mission.IsRewardCollected;
            _collectButton.gameObject.SetActive(canCollect);
            _collectButton.onClick.RemoveAllListeners();
            if (canCollect)
            {
                _collectButton.onClick.AddListener(OnCollectRewardClicked);
            }
        }

        private void OnCollectRewardClicked()
        {
            _onCollectReward?.Invoke(_mission);
            UpdateCollectButton();
        }

        private void SubscribeToMissionEvents()
        {
            _mission.OnProgressUpdated += OnProgressUpdated;
            _mission.OnMissionCompleted += OnMissionCompleted;
            _mission.OnRewardCollected += OnRewardCollected;
        }

        private void UnsubscribeFromMissionEvents()
        {
            if (_mission != null)
            {
                _mission.OnProgressUpdated -= OnProgressUpdated;
                _mission.OnMissionCompleted -= OnMissionCompleted;
                _mission.OnRewardCollected -= OnRewardCollected;
            }
        }

        private void OnProgressUpdated(float progress)
        {
            UpdateProgress();
        }

        private void OnMissionCompleted(IMission mission)
        {
            UpdateCardStatus(false, false);
            UpdateCollectButton();
        }

        private void OnRewardCollected(IMission mission)
        {
            UpdateCollectButton();
        }

        public void RefreshCard(bool isActive, bool isNext)
        {
            UpdateCardInfo();
            UpdateCardStatus(isActive, isNext);
        }

        private void OnDestroy()
        {
            //Debug.Log("DESTORYINGGGG : " + gameObject, gameObject);
            UnsubscribeFromMissionEvents();
        }
    }
}
