using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Enemies;
using Events;
using Towers;
using Towers.TowerGuns;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectileMove
{
    protected Tower _parent;
    protected float bulletSpeed;
    protected int bulletDamage;

    private void Start()
    {
        SetStats();
        StartCoroutine(Move());
    }

    private void Awake()
    {
        _parent = GetComponentInParent<Tower>();
    }

    public void SetStats()
    {
        bulletDamage = _parent.BulletDamage;
        bulletSpeed = _parent.BulletSpeed;
    }

    public virtual IEnumerator Move()
    {
        yield break;
    }
    

    public Tower GetParent()
    {
        return _parent;
    }
    
    public int GetDmg()
    {
        return bulletDamage;
    }
    
    

   
}
