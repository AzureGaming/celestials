

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
        idsDisplay = GameObject.Find("Ids").GetComponent<Text>();
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

    void UpdateIdsDisplay() {
        idsDisplay.text = "";
        foreach (int handCardId in handCardIds) {
            idsDisplay.text += handCardId.ToString() + "\n";
        }
        text.text = "Hand Counter: " + handCardIds.Count;
    }
}
