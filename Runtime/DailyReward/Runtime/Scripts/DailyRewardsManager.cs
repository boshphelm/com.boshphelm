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

        private DailyRewardSaveData _saveData = new DailyRewardSaveData();
        private bool _isInitialized;

        public event Action<DailyReward> OnRewardClaimed = delegate { };
        public event Action<TimeSpan> OnTimeUpdated = delegate { };
        public event Action<int> OnStreakUpdated = delegate { };

        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannel _onSave;  

        public void Setup()
        {
            Initialize();
            StartCoroutine(TimeUpdateRoutine());
        } 

        private void Initialize()
        {
            if (_isInitialized) return;

            // Save data yoksa yeni oluştur
            if (_saveData == null)
            {
                print("null");
                _saveData = new DailyRewardSaveData
                {
                    ClaimedDays = new List<bool>(),
                    LastClaimTime = DateTime.MinValue,
                    CurrentStreak = 0,
                    IsTodaysClaimed = false
                };

                // Claimed days listesini default rewards sayısına göre ayarla
                for (int i = 0; i < _defaultRewards.Count; i++)
                {
                    _saveData.ClaimedDays.Add(false);
                }
            }
            
            // Claimed days listesi reward sayısından küçükse tamamla
            while (_saveData.ClaimedDays.Count < _defaultRewards.Count)
            {
                _saveData.ClaimedDays.Add(false);
            }

            CheckAndResetDaily();
            _isInitialized = true;
        }

        public int GetRewardsCount()
        {
            return _defaultRewards.Count;
        }

        public bool IsRewardClaimed(int day)
        {
            if (day < 0 || day >= _saveData.ClaimedDays.Count) return false;
            return _saveData.ClaimedDays[day];
        }

        public DailyReward GetReward(int day)
        {
            if (day < 0 || day >= _defaultRewards.Count) return null;
            return _defaultRewards[day];
        }

        public int GetCurrentStreak()
        {
            return _saveData.CurrentStreak;
        }

        public void ClaimReward(int day)
        {
            if (!CanClaimReward(day)) return;

            var reward = _defaultRewards[day];
            
            // Ödülleri ver
            foreach (var price in reward.Rewards)
            {
                _wallet.AddCurrency(price);
            }

            _saveData.ClaimedDays[day] = true;
            _saveData.IsTodaysClaimed = true;
            _saveData.LastClaimTime = DateTime.Now;
            
            // Streak güncelle
            _saveData.CurrentStreak++;
            OnStreakUpdated?.Invoke(_saveData.CurrentStreak);

            OnRewardClaimed?.Invoke(reward);
            print("Claim");
            _onSave?.RaiseEvent();
        }

        public bool CanClaimReward(int day)
        {
            if (day < 0 || day >= _saveData.ClaimedDays.Count) return false;
            if (_saveData.ClaimedDays[day]) return false;
            if (_saveData.IsTodaysClaimed) return false;
            
            // Önceki günler claim edilmiş mi kontrol et
            for (int i = 0; i < day; i++)
            {
                if (!_saveData.ClaimedDays[i]) return false;
            }

            return true;
        }

        private void CheckAndResetDaily()
        {
            var now = DateTime.Now;
            var lastClaimDate = _saveData.LastClaimTime.Date;
            var resetTime = now.Date.AddHours(_resetHour);

            // Eğer son claim'den bu yana reset zamanı geçtiyse
            if (now >= resetTime && lastClaimDate < now.Date)
            {
                var daysDifference = (now.Date - lastClaimDate).Days;

                // Eğer bir günden fazla geçtiyse streak'i sıfırla
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

            // Tüm claim durumlarını resetle
            for (int i = 0; i < _saveData.ClaimedDays.Count; i++)
            {
                _saveData.ClaimedDays[i] = false;
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

        public object CaptureState()
        { 
            return _saveData;
        }

        public void RestoreState(object state)
        {
            _saveData = (DailyRewardSaveData)state;  
        }     

    }
}