using System;
using System.Collections.Generic;
using System.Timers;
using Events;
using UnityEngine;
using EventType = Events.EventType;

namespace UI.Pause
{
    public class PauseScript : MonoBehaviour
    {

        public List<GameObject> exceptPauses = new();
        
        public static List<IPausable> Pauses = new();
        public static bool IsPaused;

        public static List<IPausable> ExceptionPauses = new();

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.PAUSE, Pause);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.PAUSE, Pause);
        }

        private void Awake()
        {
            IsPaused = false;
            foreach (GameObject gm in exceptPauses)
            {
                ExceptionPauses.Add(gm.GetComponent<IPausable>());
            }
        }

        public void Pause()
        {
            IsPaused = !IsPaused;
            foreach (IPausable pause in Pauses)
            {
                pause.Pause();
            }
        }
    }
}
