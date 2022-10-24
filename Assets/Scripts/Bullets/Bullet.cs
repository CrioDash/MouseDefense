using System;
using System.Collections;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour, IBulletMove
    {
        public Tower parent;
        protected float bulletSpeed;
        protected int bulletDamage;

        private void Start()
        {
            SetStats();
            StartCoroutine(Move());
        }

        private void Awake()
        {
            parent = GetComponentInParent<Tower>();
        }

        public void SetStats()
        {
            bulletDamage = parent.bulletDamage;
            bulletSpeed = parent.bulletSpeed;
        }

        public virtual IEnumerator Move()
        {
            yield break;
        }
    

        public Tower GetParent()
        {
            return parent;
        }
    
        public int GetDmg()
        {
            return bulletDamage;
        }

        private void OnDestroy()
        {
            if(parent.GetTarget() == null)
                parent.FindTarget();
        }
    }
}
