using System;
using System.Collections;
using Towers;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        protected Tower Parent;
        protected float BulletSpeed;
        protected float BulletDamage;
        protected Rigidbody _body;
        
        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            Parent = GetComponentInParent<Tower>();
            SetStats();
        }

        public void Update()
        {
            Move();
        }

        public abstract void Move();

        public void SetStats()
        {
            BulletDamage = Parent.bulletDamage;
            BulletSpeed = Parent.bulletSpeed;
        }
        

        public float GetDmg()
        {
            return BulletDamage;
        }

    }
}
