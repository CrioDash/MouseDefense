using System;
using System.Collections;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;
using EventBus = Events.EventBus;
using EventType = Events.EventType;

namespace UI
{
    public class HpBar : MonoBehaviour
    {
        public GameObject CurrHP;
        public GameObject EndGame;
        

        private void OnEnable()
        {
           
            EventBus.Subscribe(EventType.HPCHANGE, ChangeHP);
        }
    
        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.HPCHANGE, ChangeHP);
        }

        public void ChangeHP()
        {
            StartCoroutine(VisualHpChange());
        }
        

        public IEnumerator VisualHpChange()
        {
            yield return null;
            float t = 0;
            var scale = CurrHP.transform.localScale;
            while (t<1)
            {
                CurrHP.transform.localScale = new Vector3(Mathf.Lerp(scale.x, Level.Instance.CurrentHealth/ Level.Instance.MaxHealth, t), 
                    scale.y,scale.z);
                t += Time.deltaTime*8;
                yield return null;
            }
            if (Level.Instance.CurrentHealth <= 0 && !PauseScript.IsPaused)
            {
                CurrHP.GetComponentInChildren<SpriteRenderer>().color = Color.clear;
                PauseScript.ExceptionPauses.Clear();
                EventBus.Publish(EventType.PAUSE);
                EndGame.SetActive(true);
                
            }
        }

    
    }
}
