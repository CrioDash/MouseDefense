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

        private GameObject currentPrefab;
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

        public void OnEndDrag(PointerEventData eventData)
        {
           Destroy(currentPrefab.gameObject);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("fdkgopdfkopg");
            currentPrefab = Instantiate(Prefab, Level.currentLevel.Canvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}