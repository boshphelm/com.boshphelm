using System;
using System.Collections.Generic;

namespace Boshphelm.DailyRewards
{    
    [Serializable]
    public class DailyRewardSaveData
    {
        public List<bool> ClaimedDays = new List<bool>();  // Hangi günlerin claim edildiği
        public DateTime LastClaimTime;                      // Son claim zamanı
        public int CurrentStreak;                          // Mevcut streak
        public bool IsTodaysClaimed;                       // Bugün claim yapıldı mı
    }
}
