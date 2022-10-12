using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Enemies;
using Events;
using Towers;
using Towers.TowerGuns;
using UnityEngine;

public class Bullet : MonoBehaviour, IBulletMove
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
        bulletDamage = _parent.bulletDamage;
        bulletSpeed = _parent.bulletSpeed;
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
