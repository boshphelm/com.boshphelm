using UnityEngine;

namespace Boshphelm.Items
{
    public delegate Item ItemReturnItemDetailDelegate(ItemDetail itemDetail);

    [CreateAssetMenu(menuName = "Boshphelm/EventChannels/Items/ItemReturnItemDetailEventChannel")]
    public class ItemReturnItemDetailEventChannel : ScriptableObject
    {
        public ItemReturnItemDetailDelegate OnRaiseEvent;

        public Item RaiseEvent(ItemDetail itemDetail) => OnRaiseEvent?.Invoke(itemDetail);
    }
}