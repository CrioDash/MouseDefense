using System;
using System.Security.Cryptography;
using Consumables;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ConsumableItem:MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
    {
        public Consumable.ConsumableType Type;
        public GameObject Prefab;

        private Consumable currentPrefab;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            if (!PlayerStats.Consumables.ContainsKey(Type))
                gameObject.SetActive(false);
            else
                _text.text = PlayerStats.Consumables[Type].ToString();
        }

        private void FixedUpdate()
        {
            _text.text = PlayerStats.Consumables[Type].ToString();
            if (PlayerStats.Consumables[Type]==0)
                gameObject.SetActive(false);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(currentPrefab.gameObject);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            currentPrefab = Instantiate(Prefab).GetComponent<Consumable>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}