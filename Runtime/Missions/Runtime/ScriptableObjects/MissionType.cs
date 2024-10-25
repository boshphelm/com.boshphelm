using System.Collections.Generic;
using Boshphelm.Currencies;
using Boshphelm.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Boshphelm.Missions
{
    public abstract class MissionType : ScriptableObject
    {
        public SerializableGuid missionId = SerializableGuid.NewGuid();

        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 1)]
        private void AssignNewGuid()
        {
            missionId = SerializableGuid.NewGuid();
        }

        public string missionName;
        [TextArea(3, 10)]
        public string description;
        public bool main;
        public List<Price> reward;
        public List<MissionType> requiredMissions;

        public Vector2 Position;

        public abstract IMission CreateMission();

        protected MissionInfo CreateMissionInfo()
        {
            return new MissionInfo(
                missionId,
                missionName,
                description,
                main,
                reward,
                requiredMissions?.ConvertAll(m => m.missionId)
                );
        }
    }
}
