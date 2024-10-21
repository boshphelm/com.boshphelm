using System.Collections.Generic;
using Boshphelm.Items;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Players
{
    public class PlayerItemLevel : MonoBehaviour
    {
        private readonly Dictionary<SerializableGuid, int> _itemLevels = new Dictionary<SerializableGuid, int>();

        public System.Action<SerializableGuid, int> OnItemLevelChanged = (_, _) => { };

        private const int _minItemLevel = 0;
        private const int _maxItemLevel = 10; // TODO: Set it inside ItemType

        public void SetItemLevel(ItemType itemType, int level)
        {
            int clampedLevel = Mathf.Clamp(level, _minItemLevel, _maxItemLevel);

            _itemLevels.TryAdd(itemType.Id, _minItemLevel);
            _itemLevels[itemType.Id] = clampedLevel;

            OnItemLevelChanged?.Invoke(itemType.Id, clampedLevel);
        }

        //public int GetItemLevel(ItemType itemType) => _itemLevels.TryGetValue(itemType.Id, out int level) ? level : 1;

        public object CaptureState()
        {
            var saveData = new Dictionary<string, int>();

            foreach ((var itemTypeId, int level) in _itemLevels)
            {
                saveData.Add(itemTypeId.ToHexString(), level);
            }

            return saveData;
        }

        public void RestoreState(object state)
        {
            var saveData = (Dictionary<string, int>)state;
            _itemLevels.Clear();

            foreach ((string itemTypeHexId, int level) in saveData)
            {
                var itemTypeId = SerializableGuid.FromHexString(itemTypeHexId);
                _itemLevels[itemTypeId] = level;
            }
        }
    }
}
