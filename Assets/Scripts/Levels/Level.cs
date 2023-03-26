using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Enemies.Dammer;
using Game;
using GameData;
using Levels;
using UI.Pause;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using EventBus = Events.EventBus;
using EventType = Events.EventType;
using Variables = GameData.Variables;

public abstract class Level : MonoBehaviour
{
    [Header("Level Settings")]
    public GameObject enemyContainer;
    public List<Transform> Waypoints;
    public List<GameObject> EnemyPrefabs;
    public List<Transform> DammerPositions;
    public List<Transform> DamPositions;
    public Canvas Canvas;
    public NavMeshSurface Surface;
    
    public static Level Instance;

    
    public Dictionary<Variables.EnemyType, GameObject> EnemyDict = new Dictionary<Variables.EnemyType, GameObject>();
    public float MaxHealth { set; get; }
    
    public int WaveCount { set; get; }
    
    public float CurrentHealth { set; get; }
    public int Gold { private set; get; }

    

    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        WaveCount = 0;
        foreach (GameObject gm in EnemyPrefabs)
        {
            EnemyDict.Add(gm.GetComponent<Enemy>().EnemyType, gm);
        }
        Camera.main.clearFlags = CameraClearFlags.Nothing;
        Camera.main.orthographicSize = 18 * (20.0f / 9.0f / Camera.main.aspect);
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

    public void  SetStats()
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
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            if (Gold != money)
            {
                yield return wait;
                EventBus.Publish(EventType.MONEYCHANGE);
                money = Gold;
            }
            yield return wait;
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
        WaitForSeconds wait = new WaitForSeconds(interval);
        for(int i =0; i<count; i++)
        {
            GameObject enemyGM = Instantiate(EnemyDict[type], enemyContainer.transform);
            enemyGM.transform.position = enemyContainer.transform.position;
            yield return wait;
        }
    }
    

}
