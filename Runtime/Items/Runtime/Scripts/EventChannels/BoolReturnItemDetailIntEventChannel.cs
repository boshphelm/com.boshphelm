using UnityEngine;

namespace Boshphelm.Items
{
    public delegate bool BoolReturnItemDetailDelegate(ItemDetail itemDetail, int quantity);

    [CreateAssetMenu(menuName = "Boshphelm/EventChannels/Items/BoolReturnItemDetailIntEventChannel")]
    public class BoolReturnItemDetailIntEventChannel : ScriptableObject
    {
        public BoolReturnItemDetailDelegate OnRaiseEvent;

        public bool RaiseEvent(ItemDetail itemDetail, int quantity) => OnRaiseEvent != null && OnRaiseEvent.Invoke(itemDetail, quantity);
    }
}