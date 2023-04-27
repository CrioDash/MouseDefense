using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Particles
{
    public class ParticlePool:MonoBehaviour
    {
        public GameObject BloodParticle_Prefab;
        
        private Stack<GameObject> _particles = new Stack<GameObject>();
        
        
        public void Get(Transform parent)
        {
            if (_particles.Count != 0)
            {
                GameObject particle = _particles.Pop();
                particle.SetActive(true);
                particle.transform.parent = parent;
            }
            else
            {
                Instantiate(BloodParticle_Prefab, parent);
            }
        }

        public void Add(GameObject particle)
        {
            _particles.Push(particle);
            particle.SetActive(false);
        }
    }
}