using System.Collections.Generic;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Items
{
    public static class ItemDatabase
    {
        private static Dictionary<SerializableGuid, ItemDetail> _itemDetailDictionary;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            _itemDetailDictionary = new Dictionary<SerializableGuid, ItemDetail>();

            var itemDetails = Resources.LoadAll<ItemDetail>("");
            foreach (ItemDetail itemDetail in itemDetails)
            {
                _itemDetailDictionary.Add(itemDetail.Id, itemDetail);
            }
        }

        public static ItemDetail GetItemDetailById(SerializableGuid itemDetailId)
        {
            try
            {
                return _itemDetailDictionary[itemDetailId];
            }
            catch
            {
                Debug.LogError($"Cannot find item details with id {itemDetailId}");
                return null;
            }
        }
    }
}