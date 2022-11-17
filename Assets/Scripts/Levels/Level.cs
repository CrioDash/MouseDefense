using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Events;
using Game;
using Levels;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

public abstract class Level : MonoBehaviour, ILevel
{
    [Header("Level Settings")]
    public GameObject enemyContainer;
    public List<GameObject> Waypoints;
    public List<GameObject> Enemies;

    public static Level currentLevel;
    
    [HideInInspector]
    public float MaxHealth;
    [HideInInspector]
    public float CurrentHealth;
    [HideInInspector]
    public int Gold;
    
    protected List<GameObject> enemies = new List<GameObject>();


    private void Awake()
    {
        currentLevel = this;
    }

    private void Start()
    {
        Camera.main.clearFlags = CameraClearFlags.Nothing;
        EventBus.Publish(EventType.START);
    }

    private void OnEnable()
    {
        
        EventBus.Subscribe(EventType.START, SetStats);
        EventBus.Subscribe(EventType.START, StartLevel);
        
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.START, SetStats);
        EventBus.Unsubscribe(EventType.START, StartLevel);
    }

    void SetStats()
    {
        MaxHealth = Variables.Health + PlayerStats.BonusHealth;
        TakeDamage(-MaxHealth);
        ChangeMoney(Variables.Gold + PlayerStats.BonusGold);
    }

    public void TakeDamage(float hp)
    {
        CurrentHealth-=hp;
        EventBus.Publish(EventType.HPCHANGE);
    }

    public void ChangeMoney(int money)
    {
        Gold += money;
    }

    public IEnumerator MoneyCount()
    {
        int money = Gold;
        while (true)
        {
            if (Gold != money)
            {
                yield return new WaitForSeconds(0.25f);
                EventBus.Publish(EventType.MONEYCHANGE);
                money = Gold;
            }
            yield return null;
        }
    }

    public virtual void StartLevel()
    {
        StartCoroutine(MoneyCount());
        StartCoroutine(LevelScenario());
    }

    public abstract IEnumerator LevelScenario();

}
