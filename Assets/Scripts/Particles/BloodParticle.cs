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

        //������ �������� �� ����� ������������ ��������
        private void OnEnable()
        {
            _system.Play();
            StartCoroutine(WaitTillEnd());
        }

        //���������� �������� � ���
        private void OnDisable()
        {
            if (_system.isPlaying)
            {
                _system.Stop();
                Level.Instance.ParticlePool.Add(gameObject);
            }
        }

        //�������� ������� �������� � ��� ��������� �.�. �� �������� child � ����
        private void Update()
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(45, -45, 0);
            transform.Translate(Vector3.back * 3f);
        }

        //����� ���������� ����������� ��������, �� �� ��������� � ���
        public IEnumerator WaitTillEnd()
        {
            yield return new WaitUntil(() => !_system.isPlaying);
            Level.Instance.ParticlePool.Add(gameObject);
        }

    }
}
