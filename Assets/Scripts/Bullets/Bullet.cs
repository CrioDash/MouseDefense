using System;
using System.Collections;
using Towers;
using UI.Pause;
using UnityEngine;

namespace Bullets
{
    public abstract class Bullet : MonoBehaviour
    {

        public Tower Parent;
        protected float BulletSpeed;
        protected float BulletDamage;
        protected Rigidbody _body;
        protected GameObject _target;
        
        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
           
        }

        public abstract IEnumerator Move();

        public void SetStats(Tower tower)
        {
            Parent = tower;
            BulletDamage = Parent.bulletDamage;
            BulletSpeed = Parent.bulletSpeed;
            _target = Parent.GetTarget().gameObject;
            transform.position = Parent.transform.position;
            StartCoroutine(Move());
        }

        private void OnEnable()
        {
            if(Parent!=null)
                transform.position = Parent.transform.position;
        }


        protected void ReturnToPool()
        {
            StopAllCoroutines();
            Level.Instance.BulletPool.Add(Parent.type, this);
        }

        public float GetDmg()
        {
            return BulletDamage;
        }

    }
}
