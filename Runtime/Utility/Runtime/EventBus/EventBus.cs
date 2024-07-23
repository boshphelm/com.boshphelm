using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Utility
{
    public static class EventBus<T> where T : IEvent
    {
        public static HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> eventBinding)
        {
            bindings.Add(eventBinding);
        }

        public static void Deregister(EventBinding<T> eventBinding)
        {
            bindings.Remove(eventBinding);
        }

        public static void Raise(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }

        public static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }
}