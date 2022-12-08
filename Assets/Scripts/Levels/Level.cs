using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Enemies.Dammer;
using Events;
using Game;
using Levels;
using UI.Pause;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

public abstract class Level : MonoBehaviour
{
    [Header("Level Settings")]
    public GameObject enemyContainer;
    public List<GameObject> Waypoints;
    public List<GameObject> Enemies;

    public static Level currentLevel;

    [HideInInspector] 
    public Dictionary<Variables.EnemyType, GameObject> EnemyDict = new Dictionary<Variables.EnemyType, GameObject>();
    [HideInInspector]
    public float MaxHealth;
    [HideInInspector]
    public float CurrentHealth;
    [HideInInspector]
    public int Gold;
    [HideInInspector] 
    public Canvas Canvas;
    [HideInInspector] 
    public NavMeshSurface Surface;
    [HideInInspector] 
    public GameObject DammerPositions;
    [HideInInspector] 
    public GameObject DamPositions;

    private void Awake()
    {
        currentLevel = this;
    }

    private void Start()
    {
        if (LevelSwitcher.Switcher != null)
            EventBus.Publish(EventType.PAUSE);
        Canvas = FindObjectOfType<Canvas>();
        Surface = FindObjectOfType<NavMeshSurface>();
        DammerPositions = GameObject.Find("DammerPositions");
        DamPositions = GameObject.Find("DamPositions");
        foreach (GameObject gm in Enemies)
        {
            EnemyDict.Add(gm.GetComponent<Enemy>().EnemyType, gm);
        }
        Camera.main.clearFlags = CameraClearFlags.Nothing;
        Physics.gravity = new Vector3(0, -20 * Time.deltaTime*2, 0);
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

    public virtual void  SetStats()
    {
        MaxHealth = Variables.Health + PlayerStats.BonusHealth;
        TakeDamage(-MaxHealth);
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

    public IEnumerator Wave(Variables.EnemyType type, int count, float interval)
    {
        for(int i =0; i<count; i++)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            GameObject enemyGM = Instantiate(EnemyDict[type], enemyContainer.transform);
            enemyGM.transform.position = enemyContainer.transform.position;
            yield return StartCoroutine(Wait(interval));
        }
    }

    public IEnumerator Wait(float time)
    {
        float t = 0;
        while (t<time)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            t += Time.deltaTime;
            yield return null;
            
        }
    }

}
