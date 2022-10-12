using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using EventType = Events.EventType;

namespace UI.Pause
{
    public class PauseScript : MonoBehaviour
    {

        private List<IPausable> _exceptionPauses = new List<IPausable>();
        public static List<IPausable> _pauses = new List<IPausable>();
        public static bool IsPaused = false;
        
        private void OnEnable()
        {
            EventBus.Subscribe(EventType.PAUSE, Pause);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.PAUSE, Pause);
        }

        public void Pause()
        {
            IsPaused = !IsPaused;
            foreach (IPausable pause in _pauses)
            {
                if(_exceptionPauses.Contains(pause))
                    continue;
                pause.Pause();
            }
        }
    }
}
