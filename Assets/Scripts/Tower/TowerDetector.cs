using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class TowerDetector : MonoBehaviour
{
    private Tower _parent;
    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _parent = GetComponentInParent<Tower>();
    }

    private void Start()
    {
        _collider.radius = _parent.attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy"))
            return;
        if (_parent.GetTarget() == null)
        {
            _parent.FindTarget();
        }
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
