using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UI;
using UI.Pause;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject CurrHP;
    public GameObject EndGame;
    
    private Level _level;

    private void Start()
    {
        _level = FindObjectOfType<Level>();
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(AppEventType.HPCHANGE, ChangeHP);
    }
    
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(AppEventType.HPCHANGE, ChangeHP);
    }

    public void ChangeHP()
    {
        if (_level.CurrentHealth <= 0 && !PauseScript.IsPaused)
        {
            EndGame.SetActive(true);
            GameEventBus.Publish(AppEventType.PAUSE);
            return;
        }
        StartCoroutine(VisualHpChange());
    }
    
    public IEnumerator VisualHpChange()
    {
        yield return null;
        float time = 0;
        var scale = CurrHP.transform.localScale;
        while (scale.x != _level.CurrentHealth/_level.MaxHealth)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            scale = new Vector3(Mathf.Lerp(scale.x, _level.CurrentHealth/ _level.MaxHealth, time), 
                scale.y,scale.z);
            CurrHP.transform.localScale = scale;
            time += Time.deltaTime*8;
            yield return null;
        }
    }

    
}
