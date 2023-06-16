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
            if (_particles.Count == 0)
            {
                Instantiate(BloodParticle_Prefab, parent);
            }
            else
            {
                GameObject particle = _particles.Pop();
                particle.SetActive(true);
                particle.transform.parent = parent;
            }
        }

        public void Add(GameObject particle)
        {
            particle.transform.parent = particle.transform.root;
            _particles.Push(particle);
            particle.SetActive(false);
            Debug.Log(_particles.Count);
        }
    }
}