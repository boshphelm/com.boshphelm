using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.GameEvents
{
    public class GameEventManagerBase : MonoBehaviour
    {
        protected List<IObserver> _observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(IGameEvent gameEvent)
        {
            foreach (var observer in _observers)
            {
                observer.OnNotify(gameEvent);
            }
        }
    }
}
