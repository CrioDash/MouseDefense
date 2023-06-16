using System;
using System.Collections;
using UnityEngine;

namespace Particles
{
    public class BloodParticle : MonoBehaviour
    {
        private ParticleSystem _system;
        void Awake()
        {
            if (_system == null)
                _system = GetComponentInChildren<ParticleSystem>();
            
        }

        //«апуск ожидани€ до конца проигрывани€ партикла
        private void OnEnable()
        {
            _system.Play();
            StartCoroutine(WaitTillEnd());
        }

        //ƒобавление партикла в пул
        private void OnDisable()
        {
            if (_system.isPlaying)
            {
                _system.Stop();
                Level.Instance.ParticlePool.Add(gameObject);
            }
        }

        //ќбновить поворот партикла и его положение т.к. он €вл€етс€ child у моба
        private void Update()
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(45, -45, 0);
            transform.Translate(Vector3.back * 3f);
        }

        // огда закончитс€ прогрывание партикла, то он добавитс€ в пул
        public IEnumerator WaitTillEnd()
        {
            yield return new WaitUntil(() => !_system.isPlaying);
            Level.Instance.ParticlePool.Add(gameObject);
        }

    }
}
