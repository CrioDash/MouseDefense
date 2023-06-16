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
using TowerType = GameData.Variables.TowerType;

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
            if (TowerShoot != null && GetTarget() != null && GetTarget().gameObject.activeSelf && _target.transform.position.x >-100)
            {
                TowerShoot.Shoot();
            }
            yield return new WaitForSeconds(bulletCooldown);
        }
    }

    public IEnumerator StartAnimation()
    {
        while (true)
        {
            if(TowerAnimation!=null && GetTarget()!=null && GetTarget().gameObject.activeSelf && _target.transform.position.x >-100) 
                TowerAnimation.PlayAnimation();
            yield return null;
            yield return null;
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
        if (shootType == ShootType.Ground || shootType == ShootType.Both)
        {
            Gizmos.DrawWireSphere(transform.position, attackRange*1.5f);
        }
    }

}
