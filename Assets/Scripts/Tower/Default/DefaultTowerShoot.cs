using System.Collections;
using Towers.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Towers.Default
{
    public class DefaultTowerShoot:MonoBehaviour, ITowerShoot
    {
        private Tower _tower;
        public void Shoot()
        {
            if (_tower==null)
                _tower = GetComponent<Tower>();
            if (_tower.Animator!=null)
                _tower.Animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(_tower.bulletPrefab, transform);
            bullet.transform.position = _tower.bulletSpawn.transform.position;
        }
    }
}