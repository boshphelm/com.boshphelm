using UnityEngine;
using UnityEngine.Events;

namespace Boshphelm.Utility
{
    [CreateAssetMenu(fileName = "Boshphelm/EventChannels/VoidEventChannel")]
    public class VoidEventChannel : ScriptableObject
    {
        public UnityAction OnEventRaise = () => { };

        public void RaiseEvent()
        {
            OnEventRaise.Invoke();
        }
    }
}