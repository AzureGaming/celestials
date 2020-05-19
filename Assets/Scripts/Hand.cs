using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    Text text;
    [SerializeField] public List<int> handCardIds;
    CardManager cardManager;
    Text idsDisplay;
    Card[] cards;

    private void Awake() {
        text = GetComponentInChildren<Text>();
        cardManager = FindObjectOfType<CardManager>();
        cards = GetComponentsInChildren<Card>();
    }

    private void Start() {
        handCardIds = new List<int>();
    }

    public IEnumerator DrawCard() {
        GameObject card = cardManager.CreateCard();
        card.transform.SetParent(transform);
        yield break;
    }

    public void RemoveCard(Entity entity) {
        cards = GetComponentsInChildren<Card>();
        foreach (Card card in cards) {
            if (card.GetEntity() == entity) {
                Destroy(card.gameObject);
            }
        }
    }
}
