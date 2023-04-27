using System;
using System.Security.Cryptography;
using Consumables;
using Game;
using GameData;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ConsumableItem:MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
    {
        public Consumable.ConsumableType Type;
        public GameObject Prefab;
        public bool Global;

        private Consumable currentPrefab;
        
        public TextMeshProUGUI Text { set; get; }

        private void Awake()
        {
            Text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            if (!PlayerStats.Instance.Consumables.ContainsKey(Type) || PlayerStats.Instance.Consumables[Type] == 0)
                gameObject.SetActive(false);
            else
                Text.text = PlayerStats.Instance.Consumables[Type].ToString();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(currentPrefab.gameObject);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            currentPrefab = Instantiate(Prefab).GetComponent<Consumable>();
            currentPrefab.Parent = this;
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}