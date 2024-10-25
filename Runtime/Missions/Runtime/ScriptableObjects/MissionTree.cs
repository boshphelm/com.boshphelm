using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Missions
{
    [CreateAssetMenu(fileName = "Mission Tree", menuName = "Boshphelm/Missions/MissionTree")]
    public class MissionTree : ScriptableObject
    {
        [SerializeField] private List<MissionType> _missions;

        public List<MissionType> Missions => _missions;

    #if UNITY_EDITOR
        public void AddRequiredMission(MissionType mission, MissionType requiredMission)
        {
            mission.requiredMissions.Add(requiredMission);
        }

        public void RemoveRequiredMission(MissionType mission, MissionType requiredMission)
        {
            mission.requiredMissions.Remove(requiredMission);
        }
    #endif
    }

}
