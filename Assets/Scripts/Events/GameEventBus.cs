using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class GameEventBus
    {
        public static Dictionary<AppEventType, UnityEvent> Events = new Dictionary<AppEventType, UnityEvent>();

        public static void Subscribe(AppEventType type, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(type, thisEvent);
            }
        }
    

        public static void Unsubscribe(AppEventType type, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Publish(AppEventType type)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    
    }
}
