using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Items
{
    public delegate Item ItemReturnSerializableGuidDelegate(SerializableGuid serializableGuid);

    [CreateAssetMenu(fileName = "Boshphelm/EventChannels/Items/ItemReturnSerializableGuidEventChannel")]
    public class ItemReturnSerializableGuidEventChannel : ScriptableObject
    {
        public ItemReturnSerializableGuidDelegate OnRaiseEvent;

        public Item RaiseEvent(SerializableGuid serializableGuid) => OnRaiseEvent?.Invoke(serializableGuid);
    }
}