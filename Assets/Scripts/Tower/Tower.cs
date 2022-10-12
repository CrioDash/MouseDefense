using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Towers;
using UI.Pause;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(TowerPause))]
public abstract class Tower : MonoBehaviour, ITowerShoot, ITowerLevelUp
{
    [Header("Tower Settings")] 
    public TowerType type;
    public float attackRange;

    [Header("Bullet Settings")] 
    public ShootType shootType;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletCooldown;
    public int bulletDamage;
    public GameObject bulletSpawn;
    
    protected Animator _animator;
    private Enemy _target;
    private float _bulletTime = 0;
    private bool _cd = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        StartCoroutine(StartShooting());
        StartCoroutine(RotateHead());
    }

    public void OnDisable()
    {
        StopCoroutine(StartShooting());
        StopCoroutine(RotateHead());
    }

    public void FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange);
        float tempDist = float.MaxValue;
        foreach (Collider col in enemies)
        {
            if (col.CompareTag("Enemy") && (int)shootType == (int)col.GetComponent<Enemy>().Type || shootType == ShootType.Both)
            {
                //if(Vector3.Distance(transform.position, col.transform.position) < minDist)
                 //   continue;
                 _target = col.GetComponent<Enemy>();
                return;
            }
        }
        SetTarget(null);
    }

    public void SetTarget(Enemy target)
    {
        _target = target;
    }

    public Enemy GetTarget()
    {
        return _target;
    }

    public IEnumerator StartShooting()
    {
        while (true)
        {
            yield return new WaitUntil(() => GetTarget() != null);
            Shoot();
            _cd = true;
            StartCoroutine(WaitCooldown(bulletCooldown));
            yield return new WaitUntil(() => !_cd);
        }
    }
    

    private IEnumerator WaitCooldown(float cd)
    {
        float time = 0;
        while (time < cd*10)
        {
            while (PauseScript.IsPaused)
            {
                yield return null;
            }
            time += Time.deltaTime*8;
            yield return null;
        }

        _cd = false;
    }

    public abstract IEnumerator RotateHead();

    public abstract void LevelUp();
    
    public abstract void Shoot();

    public void OnDrawGizmosSelected()
    {
        DrawRadius.DrawWireDisk(transform.position, attackRange, new Color(1,1,1,0.3f));
    }

}
