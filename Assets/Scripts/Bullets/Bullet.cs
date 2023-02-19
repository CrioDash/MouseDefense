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
        
        private void Awake()
        {
            Parent = GetComponentInParent<Tower>();
            SetStats();
        }

        public void FixedUpdate()
        {
            if(!PauseScript.IsPaused)
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
