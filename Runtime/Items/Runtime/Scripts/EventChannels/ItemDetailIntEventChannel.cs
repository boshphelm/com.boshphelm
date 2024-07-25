using UnityEngine;
using UnityEngine.Events;

namespace Boshphelm.Items
{
    [CreateAssetMenu(menuName = "Boshphelm/EventChannels/Items/ItemDetailIntEventChannel")]
    public class ItemDetailIntEventChannel : ScriptableObject
    {
        public UnityAction<ItemDetail, int> OnEventRaise = (_, _) => { };

        public void RaiseEvent(ItemDetail itemDetail, int quantity)
        {
            OnEventRaise.Invoke(itemDetail, quantity);
        }
    }
}