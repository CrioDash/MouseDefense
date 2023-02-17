using System;
using System.Collections;
using Towers;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        protected Tower Parent;
        protected float BulletSpeed;
        protected float BulletDamage;
        
        private void Awake()
        {
            Parent = GetComponentInParent<Tower>();
            SetStats();
        }

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
