using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Enemies;
using Enemies.Dammer;
using Game;
using GameData;
using Levels;
using Particles;
using UI;
using UI.Pause;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using EventBus = Events.EventBus;
using EventType = Events.EventType;
using Variables = GameData.Variables;

public abstract class Level : MonoBehaviour
{
    [Header("Level Settings")]
    public GameObject enemyContainer;
    public GameObject bulletContainer;
    public GameObject textContainer;
    public List<Transform> Waypoints;
    public List<GameObject> EnemyPrefabs;
    public List<Transform> DammerPositions;
    public List<Transform> DamPositions;

    [HideInInspector]
    public BulletObjectPool BulletPool;
    [HideInInspector]
    public EnemyObjectPool EnemyPool;
    [HideInInspector]
    public DamageTextPool TextPool;
    [HideInInspector]
    public ParticlePool ParticlePool;
    
    public UniversalRenderPipelineAsset URP;
    public Light Light;
    public Canvas CanvasText;
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
        BulletPool = GetComponent<BulletObjectPool>();
        EnemyPool = GetComponent<EnemyObjectPool>();
        TextPool = GetComponent<DamageTextPool>();
        ParticlePool = GetComponent<ParticlePool>();
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

    public void SetStats()
    {
        URP.renderScale = PlayerStats.Instance.RenderScale;
        Light.shadows = PlayerStats.Instance.IsShadows ? LightShadows.Hard : LightShadows.None;
        MaxHealth = Variables.Health + PlayerStats.Instance.BonusHealth;
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

    public abstract IEnumerator InstantiatePrefabs();

    public abstract IEnumerator LevelScenario();

    public IEnumerator Wave(Variables.EnemyType type, int count, float interval)
    {
        WaitForSeconds wait = new WaitForSeconds(interval);
        for(int i =0; i<count; i++)
        {
            EnemyPool.Get(type);
            yield return wait;
        }
    }
    

}
