using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Tiles;
using Towers;
using UI;
using UI.Pause;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(TowerPause))]
public abstract class Tower : MonoBehaviour, ITowerShoot, ITowerLevelUp
{
    [Header("Tower Settings")] 
    public TowerType type;
    public float attackRange;
    public int cost;

    [Header("Bullet Settings")] 
    public GameObject head;
    public ShootType shootType;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletCooldown;
    public float bulletDamage;
    public GameObject bulletSpawn;


    [HideInInspector] public TowerTile tile;
    [HideInInspector] public int level = 1;
    [HideInInspector] public Animator animator;
    
    private Enemy _target;
    private float _bulletTime = 0;
    private bool _cd = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void FixedUpdate()
    { 
        if (GetTarget() == null || PauseScript.IsPaused)
           return;
        if((shootType!= ShootType.Both && (int)GetTarget().Type!=(int)shootType)|| !GetTarget().CompareTag("Enemy"))
            FindTarget();
        if (!_cd)
        {
            Shoot();
            _cd = true;
        }
        if (_cd)
        {
            _bulletTime += Time.fixedDeltaTime;
            if (_bulletTime >= bulletCooldown)
            {
                _bulletTime = 0;
                _cd = false;
            }
        }
        if (GetTarget() == null || PauseScript.IsPaused)
            return;
        head.transform.LookAt(GetTarget().transform.position);
        head.transform.eulerAngles = new Vector3(0, head.transform.eulerAngles.y + 90f, 0);
    }

    public void FindTarget()
    {
        List<Collider> enemies = new List<Collider>();
        if(shootType== ShootType.Ground || shootType == ShootType.Both)
            enemies = Physics.OverlapSphere(transform.position, attackRange, 1<<7).ToList();
        if (shootType == ShootType.Air)
            enemies = Physics.OverlapBox(
                new Vector3(transform.position.x, transform.position.y + 9f, transform.position.z),
                new Vector3(attackRange, 18, attackRange) / 2, Quaternion.identity, 1<<7).ToList();
        foreach (Collider col in enemies)
        {
            if ((int)shootType == (int)col.GetComponent<Enemy>().Type || shootType == ShootType.Both)
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
    

    public abstract void LevelUp();
    
    public abstract void Shoot();

    private void OnMouseUpAsButton()
    {
        if(PauseScript.IsPaused)
            return;
        if (!TowerInfo.Info.opened)
            TowerInfo.Info.ShowSellWindow(this);
    }

    public void OnDrawGizmosSelected()
    {
        if(shootType == ShootType.Air)
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+9f, transform.position.z), 
                new Vector3(attackRange, 18, attackRange));
        if(shootType == ShootType.Ground) 
            Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
