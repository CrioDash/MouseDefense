using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Events;
using Levels;
using UnityEngine;

public abstract class Level : MonoBehaviour, ILevel
{
    [Header("Level Settings")]
    public GameObject enemyContainer;
    public List<GameObject> Waypoints;
    public List<GameObject> Enemies;
    
    public float MaxHealth;
    public float CurrentHealth;
    public float Gold;
    
    protected List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        GameEventBus.Publish(AppEventType.START);
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(AppEventType.START, SetStats);
        GameEventBus.Subscribe(AppEventType.START, StartLevel);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(AppEventType.START, SetStats);
        GameEventBus.Unsubscribe(AppEventType.START, StartLevel);
    }

    void SetStats()
    {
        MaxHealth = PlayerStats.Health + PlayerStats.BonusHealth;
        TakeDamage(-MaxHealth);
        ChangeMoney(PlayerStats.Gold + PlayerStats.BonusGold);
    }

    public void TakeDamage(float hp)
    {
        CurrentHealth-=hp;
        GameEventBus.Publish(AppEventType.HPCHANGE);
    }

    public void ChangeMoney(int money)
    {
        Gold += money;
        GameEventBus.Publish(AppEventType.MONEYCHANGE);
    }

    public virtual void StartLevel()
    {
        
        StartCoroutine(LevelScenario());
    }

    public abstract IEnumerator LevelScenario();

}
