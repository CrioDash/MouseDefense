using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
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
        if (Type == ColliderType.Sphere)
            _colliderSphere.radius = _parent.attackRange;
        else
            _colliderBox.size = new Vector3(_parent.attackRange, 18, _parent.attackRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy") || _parent.GetTarget() != null)
            return;
        _parent.FindTarget();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy") || _parent.GetTarget()==null) 
            return; 
        if (_parent.GetTarget().Equals(other.GetComponent<Enemy>()))
        {
            _parent.FindTarget();
        }
    }
}
