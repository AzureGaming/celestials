using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    CardManager cardManager;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
    }

    public Card[] GetCards() {
        return GetComponentsInChildren<Card>();
    }

    public IEnumerator DrawCard() {
        GameObject card = cardManager.CreateCard();
        card.transform.SetParent(transform);
        yield break;
    }

    public void RemoveCard(Entity entity) {
        foreach (Card card in GetComponentsInChildren<Card>()) {
            if (card.GetEntity() == entity) {
                Destroy(card.gameObject);
            }
        }
    }
}
