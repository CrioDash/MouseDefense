using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Game;
using UI.Pause;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

public class MoneyShow : MonoBehaviour
{
    public Text Text;

    private int _gold;
    
    private void OnEnable()
    {
        GameEventBus.Subscribe(EventType.MONEYCHANGE, ChangeMoneyText);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(EventType.MONEYCHANGE, ChangeMoneyText);
    }

    private void Start()
    {
        _gold = Variables.Gold;
    }

    public void ChangeMoneyText()
    {
        StartCoroutine(MoneyCoroutine());
    }

    private void Update()
    {
        
    }

    private IEnumerator MoneyCoroutine()
    {
        yield return null;
        Level curLevel = FindObjectOfType<Level>();
        float time = 0;
        while (time<1)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            _gold = Mathf.RoundToInt(Mathf.Lerp(_gold, curLevel.Gold, time));
            Text.text = _gold.ToString();
            time += Time.deltaTime*8;
            yield return null;
        }
        if (_gold != curLevel.Gold)
        {
            _gold = (int)curLevel.Gold;
            Text.text = _gold.ToString();
        }
    }
}
