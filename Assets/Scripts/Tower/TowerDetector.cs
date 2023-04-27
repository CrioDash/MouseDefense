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
        StartCoroutine(CheckTarget());
    }

    public IEnumerator CheckTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            if (_parent.GetTarget() != null)
            {
                if (!_parent.GetTarget().gameObject.activeSelf)
                {
                    _parent.SetTarget(null);
                    yield return null;
                    continue;
                }
            }
            yield return null;
        }
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
        if(!other.CompareTag("Enemy"))
            return;
        if (_parent.GetTarget() == null)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.moveType != EnemyMoveType.None && ((int) _parent.shootType == (int) enemy.moveType ||
                                                         _parent.shootType == ShootType.Both))
                _parent.SetTarget(other.GetComponent<Enemy>());
        }
        else if (_parent.GetTarget() != null && !_parent.GetTarget().gameObject.activeSelf)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.moveType != EnemyMoveType.None && ((int) _parent.shootType == (int) enemy.moveType ||
                                                         _parent.shootType == ShootType.Both))
                _parent.SetTarget(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Enemy"))
            return;
        if(_parent.GetTarget()==null)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy.moveType!=EnemyMoveType.None && ((int)_parent.shootType == (int)enemy.moveType || _parent.shootType==ShootType.Both))
                _parent.SetTarget(other.GetComponent<Enemy>());
        }
        else if (_parent.GetTarget() != null && !_parent.GetTarget().gameObject.activeSelf)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy.moveType!=EnemyMoveType.None && ((int)_parent.shootType == (int)enemy.moveType || _parent.shootType==ShootType.Both))
                _parent.SetTarget(other.GetComponent<Enemy>());
            if (_parent.GetTarget().moveType == EnemyMoveType.None)
                _parent.SetTarget(null);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_parent.GetTarget() == null)
            return;
        if (other.gameObject == _parent.GetTarget().gameObject)
            _parent.SetTarget(null);
    }
}
