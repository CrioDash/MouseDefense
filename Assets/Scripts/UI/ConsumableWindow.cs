using System;
using System.Collections;
using Events;
using UI.Pause;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    public class ConsumableWindow:MonoBehaviour
    {
        public GameObject startPos;
        public GameObject endPos;

        private bool _opened = false;

        public static ConsumableWindow Instance;

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.PAUSE, Close);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.PAUSE, Close);
        }

        public void Start()
        {
            Instance = this;
            Close();
        }

        public void ButtonClick()
        {
            if(!_opened)
                Open();
            else
                Close();
        }

        public void Open()
        {
            TowerInfo.Info.CloseWindow();
            _opened = true;
            StopCoroutine(CloseAnimation());
            StartCoroutine(OpenAnimation());
        }

        public void Close()
        {
            if (transform.position == startPos.transform.position)
                return;
            _opened = false;
            StopCoroutine(OpenAnimation());
            StartCoroutine(CloseAnimation());
        }
        
        private IEnumerator OpenAnimation()
        {
            float t = 0;
        
            while (t<1f)
            {
                transform.position = Vector3.Lerp(startPos.transform.position,endPos.transform.position, t);
                t += Time.unscaledDeltaTime*8;
                yield return null;
            }

            transform.position = endPos.transform.position;
        }

        private IEnumerator CloseAnimation()
        {
            float t = 0;
        
            while (t<1f)
            {
                transform.position = Vector3.Lerp(endPos.transform.position,startPos.transform.position, t);
                t += Time.unscaledDeltaTime*8;
                yield return null;
            }

            transform.position = startPos.transform.position;
        }
    }
}