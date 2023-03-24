using System;
using System.Collections;
using Game;
using UI;
using UnityEngine;

namespace Consumables
{
    public abstract class Consumable: MonoBehaviour
    {
        public GameObject ConsumablePrefab;
        public bool Global;
        
        protected ConsumableType _type;
        protected RaycastHit ConsumableRaycast;
        
        private GameObject _circle;
        private Material _material;
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;
        private float _delay = 0.1f;
        private float _timer = 0f;
        
        public ConsumableItem Parent { set; get; }
        
        public enum ConsumableType
        {
            Bomb,
            Poison
        }

        private void Awake()
        {
            _material = transform.GetChild(0).GetComponent<Renderer>().material;
            _spriteRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos + transform.TransformDirection(new Vector3(0, 0, 10));
            if(Global || _timer<_delay)
                return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
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
            _timer -= _delay;
        }
        
        private void OnDestroy()
        {
            if (ConsumableRaycast.collider.CompareTag("Road")||ConsumableRaycast.collider.CompareTag("Enemy"))
            {
                PlayerStats.Consumables[_type]--;
                Activate();
            }
            Parent.Text.text = PlayerStats.Consumables[Parent.Type].ToString();
            if (PlayerStats.Consumables[Parent.Type]==0)
                gameObject.SetActive(false);
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