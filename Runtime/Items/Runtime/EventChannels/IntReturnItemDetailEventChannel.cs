using UnityEngine;

namespace Boshphelm.Items
{
    public delegate int IntReturnItemDetailDelegate(ItemDetail itemDetail);

    [CreateAssetMenu(menuName = "Boshphelm/EventChannels/Items/IntReturnItemDetailEventChannel")]
    public class IntReturnItemDetailEventChannel : ScriptableObject
    {
        public IntReturnItemDetailDelegate OnRaiseEvent;

        public int RaiseEvent(ItemDetail itemDetail) => OnRaiseEvent?.Invoke(itemDetail) ?? 0;
    }
}