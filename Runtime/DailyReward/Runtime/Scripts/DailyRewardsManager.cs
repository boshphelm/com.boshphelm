using System;
using System.Collections.Generic;
using UnityEngine;
using Boshphelm.Save;
using Boshphelm.Wallets;
using Boshphelm.Utility;


namespace Boshphelm.DailyRewards
{  
    public class DailyRewardsManager : MonoBehaviour, ISaveable
    {
        [Header("Reward Settings")]
        [SerializeField] private List<DailyReward> _defaultRewards;
        [SerializeField] private float _resetHour = 0f;  
        
        [Header("Dependencies")]
        [SerializeField] private Wallet _wallet;

        private DailyRewardSaveData _saveData;
        private bool _isInitialized;

        public event Action<DailyReward> OnRewardClaimed = delegate { };
        public event Action<TimeSpan> OnTimeUpdated = delegate { };
        public event Action<int> OnStreakUpdated = delegate { };

        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannel _onSaveablesRequested;

        public void Setup()
        {
            GenerateDailyRewards();
            StartCoroutine(TimeUpdateRoutine());
        }

        private void GenerateDailyRewards()
        {
            if (_isInitialized) return;

            if (_saveData == null)
            {
                _saveData = new DailyRewardSaveData
                {
                    Rewards = new List<DailyReward>(_defaultRewards),
                    LastClaimTime = DateTime.MinValue,
                    CurrentStreak = 0,
                    IsTodaysClaimed = false
                };
            }

            CheckAndResetDaily();
            _isInitialized = true;
        }
 
        public int GetRewardsCount()
        {
            return _saveData?.Rewards?.Count ?? _defaultRewards.Count;
        }

        public bool IsRewardClaimed(int day)
        {
            if (day < 0 || day >= GetRewardsCount()) return false;
            return _saveData.Rewards[day].IsClaimed;
        } 

        public DailyReward GetReward(int day)
        { 
            if (day < 0 || day >= GetRewardsCount()) return null;
            return _saveData.Rewards[day];
        }
 
        public int GetCurrentStreak()
        {
            return _saveData.CurrentStreak;
        }

        public void ClaimReward(int day)
        {
            if (!CanClaimReward(day)) return;

            var reward = _saveData.Rewards[day];
             
             
            _wallet.AddCurrency(reward.Reward); 

            reward.IsClaimed = true;
            _saveData.IsTodaysClaimed = true;
            _saveData.LastClaimTime = DateTime.Now;
             
            _saveData.CurrentStreak++;
            OnStreakUpdated?.Invoke(_saveData.CurrentStreak);

            OnRewardClaimed?.Invoke(reward);
            _onSaveablesRequested.RaiseEvent();
        }

        public bool CanClaimReward(int day)
        {
            if (day < 0 || day >= GetRewardsCount()) return false;
            if (_saveData.Rewards[day].IsClaimed) return false;
            if (_saveData.IsTodaysClaimed) return false;
             
            for (int i = 0; i < day; i++)
            {
                if (!_saveData.Rewards[i].IsClaimed) return false;
            }

            return true;
        }

        private void CheckAndResetDaily()
        {
            var now = DateTime.Now;
            var lastClaimDate = _saveData.LastClaimTime.Date;
            var resetTime = now.Date.AddHours(_resetHour);
 
            if (now >= resetTime && lastClaimDate < now.Date)
            {
                var daysDifference = (now.Date - lastClaimDate).Days;
 
                if (daysDifference > 1)
                {
                    ResetStreak();
                }

                _saveData.IsTodaysClaimed = false;
            }
        }

        private void ResetStreak()
        {
            _saveData.CurrentStreak = 0;
            OnStreakUpdated?.Invoke(0);
 
            foreach (var reward in _saveData.Rewards)
            {
                reward.IsClaimed = false;
            }
        }

        public TimeSpan GetTimeUntilNextClaim()
        {
            var now = DateTime.Now;
            var nextReset = now.Date.AddDays(1).AddHours(_resetHour);
            if (now.Hour < _resetHour)
            {
                nextReset = now.Date.AddHours(_resetHour);
            }
            return nextReset - now;
        }

        private System.Collections.IEnumerator TimeUpdateRoutine()
        {
            while (true)
            {
                var timeLeft = GetTimeUntilNextClaim();
                OnTimeUpdated?.Invoke(timeLeft);
                yield return new WaitForSeconds(1f);
                
                CheckAndResetDaily();
            }
        }

        #region Save System Implementation
        public object CaptureState()
        {
            return _saveData;
        }

        public void RestoreState(object state)
        {
            _saveData = (DailyRewardSaveData)state;
            _isInitialized = false;
            GenerateDailyRewards();
        }
        #endregion
    }
}