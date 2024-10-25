using System.Collections.Generic;
using Boshphelm.Currencies;
using Boshphelm.Utility;

namespace Boshphelm.Missions
{
    public class MissionInfo
    {
        public SerializableGuid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Main { get; set; }
        public List<Price> Reward { get; set; }
        public List<SerializableGuid> RequiredMissions { get; set; }

        public MissionInfo(SerializableGuid id, string name, string description, bool main, List<Price> reward, List<SerializableGuid> requiredMissions = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Main = main;
            Reward = reward;
            RequiredMissions = requiredMissions ?? new List<SerializableGuid>();
        }
    }
}
