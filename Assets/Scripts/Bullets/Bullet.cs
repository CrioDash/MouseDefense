using System;
using System.Collections;
using Towers;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour, IBulletMove
    {
        protected Tower Parent;
        protected float BulletSpeed;
        protected int BulletDamage;

        private void Start()
        {
            SetStats();
            StartCoroutine(Move());
        }

        private void Awake()
        {
            Parent = GetComponentInParent<Tower>();
        }

        public void SetStats()
        {
            BulletDamage = Parent.bulletDamage;
            BulletSpeed = Parent.bulletSpeed;
        }

        public virtual IEnumerator Move()
        {
            yield break;
        }

        public int GetDmg()
        {
            return BulletDamage;
        }

        private void OnDestroy()
        {
            if(Parent.GetTarget()==null)
                Parent.FindTarget();
        }
    }
}
