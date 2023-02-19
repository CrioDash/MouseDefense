using System.Timers;
using Towers.Interfaces;
using UnityEngine;

namespace Towers.Corngun
{
    public class CorngunTowerAnimation :  MonoBehaviour, ITowerAnimation

    {
        private TowerCorngun _tower;
        public void PlayAnimation()
        {
            if (_tower == null)
                _tower = GetComponent<TowerCorngun>();
            _tower.head.transform.LookAt(_tower.GetTarget().transform.position);
            _tower.head.transform.eulerAngles = new Vector3(0, _tower.head.transform.eulerAngles.y + 90f, 0);
            _tower.Barrel.transform.Rotate(_tower.RotationSpeed*Time.fixedDeltaTime,0,  0);
        }
    }
}