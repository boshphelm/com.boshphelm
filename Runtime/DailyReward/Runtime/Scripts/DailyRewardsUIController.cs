using UnityEngine;
using TMPro;
using System;

namespace Boshphelm.DailyRewards
{
    public class DailyRewardsUIController : MonoBehaviour
    {
        [SerializeField] private DailyRewardsManager _rewardsManager;
        
        [Header("UI References")]
        [SerializeField] private GameObject _rewardsPanel;
        [SerializeField] private Transform _rewardsContainer;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _streakText;
        [SerializeField] private DailyRewardCard _rewardCardPrefab;
        
        private DailyRewardCard[] _rewardCards;
        
        private void OnEnable()
        {
            _rewardsManager.OnRewardClaimed += HandleRewardClaimed;
            _rewardsManager.OnTimeUpdated += UpdateTimeText;
            _rewardsManager.OnStreakUpdated += UpdateStreakText;
        }

        private void OnDisable()
        {
            _rewardsManager.OnRewardClaimed -= HandleRewardClaimed;
            _rewardsManager.OnTimeUpdated -= UpdateTimeText;
            _rewardsManager.OnStreakUpdated -= UpdateStreakText;
        }
 
        public void Init()
        { 
            foreach (Transform child in _rewardsContainer)
            {
                Destroy(child.gameObject);
            }

            int rewardsCount = _rewardsManager.GetRewardsCount();
            _rewardCards = new DailyRewardCard[rewardsCount]; 
            
            for (int i = 0; i < rewardsCount; i++)
            {
                var rewardData = _rewardsManager.GetReward(i);
                var cardInstance = Instantiate(_rewardCardPrefab, _rewardsContainer);
                
                cardInstance.Initialize(rewardData, i, OnRewardButtonClicked);
                UpdateCardState(cardInstance, i);
                
                _rewardCards[i] = cardInstance;
            }
        }

        private void OnRewardButtonClicked(int day)
        {
            if (!_rewardsManager.CanClaimReward(day)) return; 
            _rewardsManager.ClaimReward(day); 
        }

        private void HandleRewardClaimed(DailyReward reward)
        { 
            UpdateCardState(_rewardCards[reward.Day], reward.Day);
             
            _rewardCards[reward.Day].PlayClaimAnimation();
        }

        private void UpdateTimeText(TimeSpan timeLeft)
        {
            _timeText.text = $"{timeLeft.Hours:00}:{timeLeft.Minutes:00}:{timeLeft.Seconds:00}";
        }

        private void UpdateStreakText(int streak)
        {
            _streakText.text = $"Streak: {streak} days";
        }

        private void UpdateCardState(DailyRewardCard card, int day)
        {
            bool canClaim = _rewardsManager.CanClaimReward(day);
            bool isClaimed = _rewardsManager.IsRewardClaimed(day);
            
            card.UpdateState(canClaim, isClaimed);
        }

        public void Show()
        {
            _rewardsPanel.SetActive(true);
        }

        public void Hide()
        {
            _rewardsPanel.SetActive(false);
        }
    }
}