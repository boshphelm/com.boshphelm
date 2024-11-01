using System;
using Boshphelm.Utility;
using DG.Tweening;
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
        [SerializeField] private ProgressBar _progressBar; 
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private Button _collectButton;
        [SerializeField] private GameObject _progressPanel;
        [SerializeField] private GameObject _nextMissionLabel;
        [SerializeField] private GameObject _mainMissionGO;
        [SerializeField] private GameObject _lockedMissionGO;

        private IMission _mission;
        private Action<IMission> _onCollectReward;

        public IMission Mission => _mission;

        public void SetupCard(IMission mission, bool isActive, bool isNext, Action<IMission> onCollectReward)
        {
            _mission = mission;
            _onCollectReward = onCollectReward;

            UpdateCardInfo();
            UpdateProgress();
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
        public void UpdateCardStatus(bool visible, bool isActive, bool isNext)
        {
            UpdateCardStatus(isActive, isNext);
            gameObject.SetActive(visible);
        }
        private void UpdateCardStatus(bool isActive, bool isNext)
        {
            //Debug.Log("IS ACTIVE : " + isActive + ", IS NEXT : " + isNext);
            if (_mission.IsCompleted)
            {
                gameObject.SetActive(true);
                _lockedMissionGO.SetActive(false);
            }
            else if (isActive)
            {
                gameObject.SetActive(true);
                _lockedMissionGO.SetActive(false);
            }
            else if (isNext)
            {
                gameObject.SetActive(true);
                _lockedMissionGO.SetActive(false);
            }
            else if (_mission.IsFinished)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _lockedMissionGO.SetActive(true);
                gameObject.SetActive(true);
            }

            _mainMissionGO.SetActive(_mission.Main);

            _nextMissionLabel.SetActive(!isActive && isNext);
        }

        private void UpdateProgress()
        {
            float progress = _mission.Progress;
            _progressBar.SetValue(progress);  
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
            var targetScale = transform.localScale;
            transform.DOScale(targetScale * 1.05f, .2f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _progressPanel.SetActive(false);
                    _collectButton.gameObject.SetActive(false);
                    _descriptionText.text = "";
                    transform.DOScale(targetScale, .1f).SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            _onCollectReward?.Invoke(_mission);
                            UpdateCollectButton();
                        });
                });
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
            // UpdateCardStatus(false, false);
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
