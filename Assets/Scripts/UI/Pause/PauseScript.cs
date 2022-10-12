using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace UI.Pause
{
    public class PauseScript : MonoBehaviour
    {
        public List<GameObject> Exceptions = new List<GameObject>();

        private List<IPausable> _exceptionPauses = new List<IPausable>();
        public static List<IPausable> _pauses = new List<IPausable>();
        public static bool IsPaused = false;

        public void Start()
        {
            foreach(GameObject gm in Exceptions)
                _exceptionPauses.Add(gm.GetComponent<IPausable>());
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe(AppEventType.PAUSE, Pause);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe(AppEventType.PAUSE, Pause);
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
