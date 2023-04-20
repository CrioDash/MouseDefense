using System;
using System.Collections;
using Events;
using UI.Pause;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    public class LoadingScreen:MonoBehaviour
    {

        public static LoadingScreen Instance;
            
        public bool Fade = false;
        
        private SpriteRenderer _renderer;
        

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            
        }

        private void Start()
        {
            Instance = this;
            _renderer.color = Fade ? Color.black : Color.clear;
        }


        public IEnumerator CloseAnimation()
        {
            _renderer.color = Color.black;
            yield return new WaitForSecondsRealtime(0.5f);
            float t = 0;
            while (t<1)
            {
                _renderer.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), t);
                t += Time.unscaledDeltaTime*4;
                yield return null;
            }
            _renderer.color = Color.clear;
            if(PauseScript.IsPaused)
                EventBus.Publish(EventType.PAUSE);
        }
        
        public IEnumerator ShowAnimation()
        {
            _renderer.color = Color.clear;
            float t = 0;
            while (t<1)
            {
                _renderer.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), t);
                t += Time.unscaledDeltaTime*4;
                yield return null;
            }
            _renderer.color = Color.black;
            if(PauseScript.IsPaused)
                EventBus.Publish(EventType.PAUSE);
        }
    }
}