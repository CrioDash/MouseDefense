using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using EventType = Events.EventType;

namespace UI.Pause
{
    public class PauseScript : MonoBehaviour
    {

        public List<GameObject> ExceptPauses = new List<GameObject>();
        
        public static List<IPausable> _pauses = new List<IPausable>();
        public static bool IsPaused;

        public static List<IPausable> ExceptionPauses = new List<IPausable>();

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
            foreach (GameObject gm in ExceptPauses)
            {
                ExceptionPauses.Add(gm.GetComponent<IPausable>());
            }
        }

        public void Pause()
        {
            IsPaused = !IsPaused;
            foreach (IPausable pause in _pauses)
            {
                if(ExceptionPauses.Contains(pause))
                    continue;
                pause.Pause();
            }
        }
    }
}
