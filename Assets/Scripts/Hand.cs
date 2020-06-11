using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hand : MonoBehaviour, IDropHandler {
    CardManager cardManager;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
    }

    public Card[] GetCards() {
        return GetComponentsInChildren<Card>();
    }

    public void RemoveCard(Entity entity) {
        foreach (Card card in GetComponentsInChildren<Card>()) {
            if (card.GetEntity() == entity) {
                Destroy(card.gameObject);
            }
        }
    }

    public void OnDrop(PointerEventData eventData) {
        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
