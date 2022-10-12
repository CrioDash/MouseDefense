using System.Collections;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour, IBulletMove
    {
        protected Tower _parent;
        protected float bulletSpeed;
        protected int bulletDamage;

        private void Start()
        {
            SetStats();
            StartCoroutine(Move());
        }

        private void Awake()
        {
            _parent = GetComponentInParent<Tower>();
        }

        public void SetStats()
        {
            bulletDamage = _parent.bulletDamage;
            bulletSpeed = _parent.bulletSpeed;
        }

        public virtual IEnumerator Move()
        {
            yield break;
        }
    

        public Tower GetParent()
        {
            return _parent;
        }
    
        public int GetDmg()
        {
            return bulletDamage;
        }
    
    

   
    }
}
