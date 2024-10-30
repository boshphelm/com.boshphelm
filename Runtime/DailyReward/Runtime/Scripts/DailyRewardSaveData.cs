using System;
using System.Collections.Generic;

namespace Boshphelm.DailyRewards
{
    [Serializable]
    public class DailyRewardSaveData
    {
        public List<DailyReward> Rewards;
        public DateTime LastClaimTime;
        public int CurrentStreak;
        public bool IsTodaysClaimed;
    }
}
