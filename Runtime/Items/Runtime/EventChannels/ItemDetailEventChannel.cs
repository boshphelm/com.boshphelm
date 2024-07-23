using UnityEngine;
using UnityEngine.Events;

namespace Boshphelm.Items
{
    [CreateAssetMenu(menuName = "Boshphelm/EventChannels/Items/ItemDetailEventChannel")]
    public class ItemDetailEventChannel : ScriptableObject
    {
        public readonly UnityAction<ItemDetail> OnEventRaise = _ => { };

        public void RaiseEvent(ItemDetail itemDetail)
        {
            OnEventRaise.Invoke(itemDetail);
        }
    }
}