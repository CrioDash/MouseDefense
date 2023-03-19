using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Game;
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
    public SpriteRenderer radiusSprite;
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
    
    protected TowerDetector _detector;
    
    private Enemy _target;
    
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        _detector = GetComponentInChildren<TowerDetector>();
        radiusSprite.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 1);
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
                new Vector3(transform.position.x, transform.position.y + attackRange*0.75f, transform.position.z),
                new Vector3(attackRange* 1.5f, attackRange*1.5f, attackRange* 1.5f) / 2, Quaternion.identity, 1<<7).ToList();
        foreach (Collider col in enemies)
        {
            if (((int)shootType == (int)col.GetComponent<Enemy>().Type || shootType == ShootType.Both) && col.GetComponent<Enemy>().Type!=EnemyType.None)
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
        if (TowerInfo.Info.IsOpened)
        {
            ConsumableWindow.Instance.Close();
            TowerInfo.Info.CloseWindow();
        }

        if (!TowerInfo.Info.IsOpened)
        {
            Color clr = radiusSprite.color;
            clr.a = 0.1f;
            radiusSprite.color = clr;
            TowerInfo.Info.ShowSellWindow(this);
        }
    }

    public void OnDrawGizmosSelected()
    {
        if(shootType == ShootType.Air)
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+attackRange*1.5f/2, transform.position.z), 
                new Vector3(attackRange*1.5f, attackRange*1.5f, attackRange*1.5f));
        if(shootType == ShootType.Ground || shootType == ShootType.Both) 
            Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
