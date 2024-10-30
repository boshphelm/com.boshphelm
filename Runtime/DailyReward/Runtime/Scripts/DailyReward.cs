using System;
using System.Collections.Generic;
using Boshphelm.Currencies;

namespace Boshphelm.DailyRewards
{
    [Serializable]
    public class DailyReward
    {
        public int Day;
        public Price Reward;
        public bool IsClaimed;
    }
}
