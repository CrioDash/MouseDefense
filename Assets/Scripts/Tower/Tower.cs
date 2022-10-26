using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject head;
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
        
    }

    public void FindTarget()
    {
        List<Collider> enemies = new List<Collider>();
        if(shootType== ShootType.Ground)
           enemies = Physics.OverlapSphere(transform.position, attackRange, 1<<7).ToList();
        if (shootType == ShootType.Air)
            enemies = Physics.OverlapBox(
                new Vector3(transform.position.x, transform.position.y + 9f, transform.position.z),
                new Vector3(attackRange, 18, attackRange) / 2, Quaternion.identity, 1<<7).ToList();
        foreach (Collider col in enemies)
        {
            if (col.CompareTag("Enemy") && (int)shootType == (int)col.GetComponent<Enemy>().Type || shootType == ShootType.Both)
            {
                SetTarget(col.GetComponent<Enemy>());
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
            while (GetTarget() == null || PauseScript.IsPaused)
            {
                yield return null;
            }
            //if(shootType!= ShootType.Both && (int)GetTarget().Type != (int)shootType)
             //   FindTarget();
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
    

    public IEnumerator RotateHead()
    {
        while (true)
        {
            while (GetTarget() == null || PauseScript.IsPaused)
            {
                yield return null;
            }
            head.transform.LookAt(GetTarget().transform.position);
            head.transform.eulerAngles = new Vector3(0, head.transform.eulerAngles.y + 90f, 0);
            yield return null;
        }
    }


    public abstract void LevelUp();
    
    public abstract void Shoot();

    public void OnDrawGizmosSelected()
    {
        if(shootType == ShootType.Air)
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+9f, transform.position.z), 
                new Vector3(attackRange, 18, attackRange));
        if(shootType == ShootType.Ground) 
            Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
