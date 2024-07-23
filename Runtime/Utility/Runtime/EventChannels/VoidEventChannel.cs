using UnityEngine;
using UnityEngine.Events;

namespace Boshphelm.Utility
{
    [CreateAssetMenu(menuName = "Boshphelm/EventChannels/VoidEventChannel")]
    public class VoidEventChannel : ScriptableObject
    {
        public UnityAction OnEventRaise = () => { };

        public void RaiseEvent()
        {
            OnEventRaise.Invoke();
        }
    }
}