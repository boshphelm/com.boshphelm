using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Boshphelm.Utility
{
    public static class EventBusUtil
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> EventBusTypes { get; set; }

#if UNITY_EDITOR
        public static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            PlayModeState = state;
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                ClearAllBuses();
            }
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        private static IReadOnlyList<Type> InitializeAllBuses()
        {
            var eventBusTypes = new List<Type>();

            Type typedef = typeof(EventBus<>);
            foreach (Type eventType in EventTypes)
            {
                Type busType = typedef.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
            }

            return eventBusTypes;
        }

        public static void ClearAllBuses()
        {
            //Debug.Log("CLEARING ALL BUSSES");
            for (int i = 0; i < EventBusTypes.Count; i++)
            {
                Type busType = EventBusTypes[i];
                MethodInfo clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod?.Invoke(null, null);
            }
        }
    }
}