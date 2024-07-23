using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Boshphelm.Save
{
    [CreateAssetMenu(menuName = "Boshphelm/SaveSystem/EventChannels/Saveable List Event Channel")]
    public class SaveableListEventChannelSO : ScriptableObject
    {
        public UnityAction<List<SaveableEntity>> onEventRaised;

        public void RaiseEvent(List<SaveableEntity> saveables)
        {
            if (onEventRaised != null)
                onEventRaised.Invoke(saveables);
        }
    }
}