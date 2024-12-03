using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.GFASystem
{
    public abstract class GameFlowAction : MonoBehaviour
    {
        protected List<IGameFlowActionListener> listeners = new List<IGameFlowActionListener>();

        public System.Action<GameFlowAction> onActionComplete;
        public System.Action<GameFlowAction> onActionStarted;

        public void RegisterListener(IGameFlowActionListener listener)
        {
            if (listeners.Contains(listener)) return;

            listeners.Add(listener);
        }

        public void UnregisterListener(IGameFlowActionListener listener)
        {
            if (!listeners.Contains(listener)) return;

            listeners.Remove(listener);
        }

        public virtual void StartAction()
        {
            onActionStarted?.Invoke(this);
        }

        public virtual void CompleteAction()
        {
            onActionComplete?.Invoke(this);
            BroadcastActionCompleteToAllListeners();
        }

        protected void BroadcastActionCompleteToAllListeners()
        {
            for (int i = 0; i < listeners.Count; i++) listeners[i].OnGameFlowActionComplete(this);
        }
    }
}