using System;
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
            foreach (var itemDetail in itemDetails)
            {
                _itemDetailDictionary.Add(itemDetail.Id, itemDetail);
            }
        }

        public static TItemDetail GetItemDetailById<TItemDetail>(SerializableGuid itemDetailId) where TItemDetail : ItemDetail
        {
            try
            {
                var itemDetail = _itemDetailDictionary[itemDetailId];

                if (itemDetail is not TItemDetail secureItemDetail)
                {
                    throw new Exception($"Item Detail : {itemDetail.name} is not of type {typeof(TItemDetail).Name}");
                }

                return secureItemDetail;
            }
            catch
            {
                Debug.LogError($"Cannot find item details with id {itemDetailId}");
                return null;
            }
        }
    }
}
