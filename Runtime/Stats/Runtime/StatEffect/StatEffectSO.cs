using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Stats
{
    public abstract class StatEffectSO<T> : ScriptableObject, ISerializationCallbackReceiver where T : StatType
    {
        [SerializeField] private string _id;
        public string ID => _id;

        [SerializeField] private StatEffect<T>[] _statEffects;
        public StatEffect<T>[] StatEffects => _statEffects;

        private static Dictionary<string, StatEffectSO<T>> _itemLookupCache;

        public static StatEffectSO<T> GetFromID(string id)
        {
            if (_itemLookupCache == null) ReFillTheCache();

            if (id == null || !_itemLookupCache.ContainsKey(id)) return null;
            return _itemLookupCache[id];
        }

        private static void ReFillTheCache()
        {
            _itemLookupCache = new Dictionary<string, StatEffectSO<T>>();
            var itemList = Resources.LoadAll<StatEffectSO<T>>("");
            foreach (var item in itemList)
            {
                if (_itemLookupCache.ContainsKey(item.ID))
                {
                    Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", _itemLookupCache[item.ID], item));
                    continue;
                }

                _itemLookupCache[item.ID] = item;
            }
        }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrWhiteSpace(ID)) _id = System.Guid.NewGuid().ToString();
        }

        public void OnBeforeSerialize()
        {
        }
    }
}