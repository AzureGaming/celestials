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
        foreach (Card card in GetCards()) {
            Transform cardElementContainer = card.transform.Find("Card Element Container");
            RectTransform rt = cardElementContainer.GetComponent<RectTransform>();
            rt.offsetMax = new Vector2(0, 0); // top
            rt.offsetMin = new Vector2(0, 0); //bot
            //rt.offsetMin = new Vector2(0, rt.offsetMin.y); //left
            //rt.offsetMin = new Vector2(0, rt.offsetMax.y); //right
        }
        Debug.Log("Done recalculation");
        //LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        foreach (Card card in GetCards()) {
            card.GetComponent<UIHoverSize>().enabled = true;
        }
    }
}
