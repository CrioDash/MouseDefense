using System;
using System.Collections;
using Game;
using GameData;
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
        protected float Radius;
        protected SpriteRenderer ColorCircle;
        protected SpriteRenderer RadiusCircle;
        
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
            ColorCircle = transform.GetChild(0).GetComponent<SpriteRenderer>();
            RadiusCircle = transform.GetChild(1).GetComponent<SpriteRenderer>();
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
            Physics.Raycast(ray, out ConsumableRaycast);
            if(ConsumableRaycast.collider==null)
                return;
            if (ConsumableRaycast.collider.CompareTag("Road"))
                ChangeColor(new Color(0,1,0,0.4f));
            else
                ChangeColor(new Color(1,0,0,0.4f));
            _timer -= _delay;
        }
        
        private void OnDestroy()
        {
            if (ConsumableRaycast.collider.CompareTag("Road"))
            {
                PlayerStats.Instance.Consumables[_type]--;
                Activate();
            }
            Parent.Text.text = PlayerStats.Instance.Consumables[Parent.Type].ToString();
            if (PlayerStats.Instance.Consumables[Parent.Type]==0)
                Parent.gameObject.SetActive(false);
        }

        public void ChangeColor(Color color)
        {
            ColorCircle.color = color;
        }

        public abstract void Activate();

        public static void Add(ConsumableType type, int count)
        {
            if (PlayerStats.Instance.Consumables.ContainsKey(type))
                PlayerStats.Instance.Consumables[type] += count;
            else
                PlayerStats.Instance.Consumables.Add(type, count);
        }
    }
}