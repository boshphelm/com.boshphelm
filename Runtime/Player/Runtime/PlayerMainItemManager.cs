using System.Collections.Generic;
using Boshphelm.Items;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Players
{
    public class PlayerMainItem : MonoBehaviour
    {
        private readonly Dictionary<SerializableGuid, ItemDetail> _mainItems = new Dictionary<SerializableGuid, ItemDetail>();

        public System.Action<ItemType, ItemDetail> OnMainItemChanged = (_, _) => { };

        public void SetMainItem(ItemType itemType, ItemDetail itemDetail)
        {
            _mainItems.TryAdd(itemType.Id, null);
            _mainItems[itemType.Id] = itemDetail;

            OnMainItemChanged?.Invoke(itemType, itemDetail);
        }

        public ItemDetail GetMainItem(ItemType itemType) => _mainItems.TryGetValue(itemType.Id, out var itemDetail) ? itemDetail : null;

        public object CaptureState()
        {
            var saveData = new Dictionary<string, string>();
            foreach (var (itemTypeId, itemDetail) in _mainItems)
            {
                saveData[itemTypeId.ToHexString()] = itemDetail.Id.ToHexString();
            }
            return saveData;
        }

        public void RestoreState(object state)
        {
            var saveData = (Dictionary<string, string>)state;
            _mainItems.Clear();
            foreach ((string itemTypeHexId, string mainItemDetailHexId) in saveData)
            {
                var itemDetailId = SerializableGuid.FromHexString(mainItemDetailHexId);
                var itemDetail = ItemDatabase.GetItemDetailById(itemDetailId);
                if (itemDetail == null) continue;

                var itemTypeId = SerializableGuid.FromHexString(itemTypeHexId);
                _mainItems.TryAdd(itemTypeId, itemDetail);
            }
        }
    }
}
