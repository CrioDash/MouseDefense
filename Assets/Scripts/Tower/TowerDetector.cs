using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Enemies;
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

    public void UpdateColliders()
    {
        if (Type == ColliderType.Sphere)
            _colliderSphere.radius = _parent.attackRange;
        else
        {
            _colliderBox.center = new Vector3(0, _parent.attackRange * 0.75f, 0);
            _colliderBox.size = new Vector3(_parent.attackRange* 1.5f, _parent.attackRange * 1.5f, _parent.attackRange* 1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy") || _parent.GetTarget() != null)
            return;
        _parent.FindTarget();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && (_parent.GetTarget()==null || _parent.GetTarget().Type == EnemyType.None))
        {
            _parent.SetTarget(null);
            _parent.FindTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy") || _parent.GetTarget()==null) 
            return;
        _parent.SetTarget(null);
        _parent.FindTarget();
    }
}
