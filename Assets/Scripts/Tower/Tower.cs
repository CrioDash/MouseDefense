using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Tiles;
using Towers;
using Towers.Interfaces;
using UI;
using UI.Pause;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(TowerPause))]
public abstract class Tower : MonoBehaviour
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


    public TowerTile tile { set; get; }
    public int Level { set; get; } = 1;
    public Animator Animator { set; get; }

    public ITowerShoot TowerShoot { set; get; }
    public ITowerAnimation TowerAnimation { set; get; }
    
    private Enemy _target;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        StartCoroutine(StartShooting());
        StartCoroutine(StartAnimation());
    }
    

    public void FindTarget()
    {
        if(GetTarget()!=null)
            return;
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
                Debug.Log("Found");
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
            if(TowerShoot!=null && GetTarget()!=null)
                TowerShoot.Shoot();
            yield return new WaitForSeconds(bulletCooldown);
        }
    }

    public IEnumerator StartAnimation()
    {
        while (true)
        {
            if(TowerAnimation!=null && GetTarget()!=null)
                TowerAnimation.PlayAnimation();
            yield return null;
        }
    }

    public abstract void LevelUp();
    

    private void OnMouseUpAsButton()
    {
        if(PauseScript.IsPaused)
            return;
        if (!TowerInfo.Info.IsOpened)
            TowerInfo.Info.ShowSellWindow(this);
    }

    public void OnDrawGizmosSelected()
    {
        if(shootType == ShootType.Air)
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+9f, transform.position.z), 
                new Vector3(attackRange, 18, attackRange));
        if(shootType == ShootType.Ground || shootType == ShootType.Both) 
            Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
