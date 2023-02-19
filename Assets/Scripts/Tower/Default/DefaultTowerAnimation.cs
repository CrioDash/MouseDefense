using Towers.Interfaces;
using UnityEngine;

namespace Towers.Default
{
    public class DefaultTowerAnimation : MonoBehaviour, ITowerAnimation
    {
        private Tower _tower;
        public void PlayAnimation()
        {
            if (_tower == null)
                _tower = GetComponent<Tower>();
            _tower.head.transform.LookAt(_tower.GetTarget().transform.position);
            _tower.head.transform.eulerAngles = new Vector3(0, _tower.head.transform.eulerAngles.y + 90f, 0);
        }
    }
}