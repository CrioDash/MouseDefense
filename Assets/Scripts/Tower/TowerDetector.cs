using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Enemies;
using Towers;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDetector : MonoBehaviour
{
    public enum ColliderType
    {
        Sphere, Box
    }

    public ColliderType Type;
    
    private Tower _parent;
    private SphereCollider _colliderSphere;
    private BoxCollider _colliderBox;

    private void Awake()
    {
        if (Type == ColliderType.Sphere)
            _colliderSphere = GetComponent<SphereCollider>();
        else
            _colliderBox = GetComponent<BoxCollider>();
        _parent = GetComponentInParent<Tower>();
    }

    private void Start()
    {
        UpdateColliders();
    }

    private void Update()
    {
        if(_parent.GetTarget()==null)
            return;
        if(_parent.GetTarget().gameObject.activeSelf)
            return;
        _parent.SetTarget(null);
    }

    public void UpdateColliders()
    {
        if (Type == ColliderType.Sphere)
        {
            _colliderSphere.enabled = false;
            _colliderSphere.radius = _parent.attackRange;
            _colliderSphere.enabled = true;
        }
        else
        {
            _colliderBox.enabled = false;
            _colliderBox.center = new Vector3(0, _parent.attackRange * 0.75f, 0);
            _colliderBox.size = new Vector3(_parent.attackRange* 1.5f, _parent.attackRange * 1.5f, _parent.attackRange* 1.5f);
            _colliderBox.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_parent.GetTarget()!=null)
            if(!other.CompareTag("Enemy") || _parent.GetTarget().gameObject.activeSelf) 
                return;
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy.moveType!=EnemyMoveType.None && ((int)_parent.shootType == (int)enemy.moveType || _parent.shootType==ShootType.Both))
            _parent.SetTarget(other.GetComponent<Enemy>());
    }

    private void OnTriggerStay(Collider other)
    {
        if(_parent.GetTarget()==null)
            return;
        if (other.CompareTag("Enemy") && (!_parent.GetTarget().gameObject.activeSelf || _parent.GetTarget().moveType == EnemyMoveType.None))
        {
            _parent.SetTarget(null);
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy.moveType!=EnemyMoveType.None && ((int)_parent.shootType == (int)enemy.moveType || _parent.shootType==ShootType.Both))
                _parent.SetTarget(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_parent.GetTarget()==null)
            return;
        if (!other.CompareTag("Enemy") || !_parent.GetTarget().gameObject.activeSelf) 
            return;
        _parent.SetTarget(null);
    }
}
