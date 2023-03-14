using System;
using Game;
using UnityEngine;

namespace Consumables
{
    public abstract class Consumable: MonoBehaviour
    {
        public GameObject ConsumablePrefab;
        
        protected ConsumableType _type;
        protected RaycastHit ConsumableRaycast;
        
        private GameObject _circle;
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
            _spriteRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = ray.origin;
            transform.position = pos + transform.TransformDirection(new Vector3(0, 0, 10));
            pos.y -= 8f;
            pos += transform.TransformDirection(new Vector3(0, 0, 10));
            ray.origin = pos;
            
            Debug.DrawRay(ray.origin, ray.direction);
            Physics.Raycast(ray, out ConsumableRaycast);
            if(ConsumableRaycast.collider==null)
                return;
            if (ConsumableRaycast.collider.CompareTag("Road")|ConsumableRaycast.collider.CompareTag("Enemy"))
                ChangeColor(Color.green);
            else
                ChangeColor(Color.red);
           
        }
        
        private void OnDestroy()
        {
            if (ConsumableRaycast.collider.CompareTag("Road")||ConsumableRaycast.collider.CompareTag("Enemy"))
            {
                PlayerStats.Consumables[_type]--;
                Activate();
            }
        }

        public void ChangeColor(Color color)
        {
            _material.color = color;
            _spriteRenderer.color = color;
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