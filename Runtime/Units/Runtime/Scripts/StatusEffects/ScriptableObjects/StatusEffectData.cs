using UnityEngine;
using Boshphelm.Utility;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Boshphelm.Units
{

    public abstract class StatusEffectData : ScriptableObject
    {
        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 0.8f, 1)]
        private void AssignNewGuid()
        {
            Id = SerializableGuid.NewGuid();
        }
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public string Name;
        public string Description;
        public Sprite Icon;
        public bool IsStackable;
        public bool IsPermanent;
        public List<StatusEffectLevelData> Levels = new List<StatusEffectLevelData>();

        public StatusEffectLevelData GetLevelData(int level)
        {
            return Levels.Find(l => l.Level == level) ?? Levels[0];
        }
    }
}
