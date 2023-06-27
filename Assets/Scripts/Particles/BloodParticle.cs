using System;
using System.Collections;
using UnityEngine;

namespace Particles
{
    public class BloodParticle : Particle
    {
        private ParticleSystem _system;
        void Awake()
        {
            if (_system == null)
                _system = GetComponentInChildren<ParticleSystem>();
            
        }


        //�������� ������� �������� � ��� ��������� �.�. �� �������� child � ����
        private void Update()
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(45, -45, 0);
            transform.Translate(Vector3.back * 3f);
        }
        
    }
}
