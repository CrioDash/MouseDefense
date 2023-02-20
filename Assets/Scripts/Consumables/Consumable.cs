using System;
using Game;
using UnityEngine;

namespace Consumables
{
    public abstract class Consumable: MonoBehaviour
    {
        public RaycastHit RaycastHit { set; get; }

        private Material _material;
        private SpriteRenderer _spriteRenderer;
        
        public enum ConsumableType
        {
            Bomb,
            Poison
        }

        private void Awake()
        {
            _material = transform.GetChild(0).GetComponent<Renderer>().material;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Debug.Log(_spriteRenderer.color);
        }

        private void FixedUpdate()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos += transform.TransformDirection(0, 0, 10);
            transform.position = pos;
        }

        public abstract void Activate();

        public static void Add(ConsumableType type, int count)
        {
            if (PlayerStats.Consumables.ContainsKey(type))
                PlayerStats.Consumables[type] += count;
            else
                PlayerStats.Consumables.Add(type, count);
        }
    }
}