using System.Collections;
using System.Collections.Generic;
using Boshphelm.Currencies;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Missions
{
    public class MissionInfo
    {
        public SerializableGuid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Price> Reward { get; set; }
        public List<SerializableGuid> RequiredMissions { get; set; }

        public MissionInfo(SerializableGuid id, string name, string description, List<Price> reward, List<SerializableGuid> requiredMissions = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Reward = reward;
            RequiredMissions = requiredMissions ?? new List<SerializableGuid>();
        }
    }
}
