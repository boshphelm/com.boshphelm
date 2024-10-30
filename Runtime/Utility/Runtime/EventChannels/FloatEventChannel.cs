using UnityEngine;
using UnityEngine.Events;

namespace Boshphelm.Utility
{
    [CreateAssetMenu(menuName = "Boshphelm/EventChannels/FloatEventChannel")]
    public class FloatEventChannel : ScriptableObject
    {
        public UnityAction<float> OnEventRaise;

        public void RaiseEvent(float value)
        {
            OnEventRaise?.Invoke(value);
        }
    }
}