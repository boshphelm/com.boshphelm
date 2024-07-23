using UnityEngine;
using UnityEngine.Events;
namespace Boshphelm.Utility
{
    [CreateAssetMenu(menuName = "Boshphelm/Events/Channels/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    {
        public UnityAction onEventRaised;

        public void RaiseEvent()
        {
            if (onEventRaised != null)
                onEventRaised.Invoke();
        }
    }
}