using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using EventType = Events.EventType;

namespace UI.Pause
{
    public class PauseScript : MonoBehaviour
    {

        public List<GameObject> Pauses = new List<GameObject>();
        
        public static List<IPausable> _pauses = new List<IPausable>();
        public static bool IsPaused;

        private static List<IPausable> _exceptionPauses = new List<IPausable>();

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.PAUSE, Pause);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.PAUSE, Pause);
        }

        private void Start()
        {
            IsPaused = false;
            foreach (GameObject gm in Pauses)
            {
                _exceptionPauses.Add(gm.GetComponent<IPausable>());
            }
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
