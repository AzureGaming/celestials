﻿

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    Text text;
    [SerializeField] public List<int> handCardIds;
    CardManager cardManager;
    HandSlot[] handSlots;
    Text idsDisplay;
    Card[] cards;

    private void Awake() {
        text = GetComponentInChildren<Text>();
        cardManager = FindObjectOfType<CardManager>();
        handSlots = GetComponentsInChildren<HandSlot>();
        idsDisplay = GameObject.Find("Ids").GetComponent<Text>();
        cards = GetComponentsInChildren<Card>();
    }

    private void Start() {
        handCardIds = new List<int>();
        foreach (HandSlot handSlot in handSlots) {
            handSlot.DestroyChildren();
        }
    }

    public IEnumerator DrawCard() {
        GameObject card = cardManager.CreateCard();
        card.transform.SetParent(transform);
        yield break;
    }

    public void RemoveCard(int id) {
        cards = GetComponentsInChildren<Card>();
        foreach (Card card in cards) {
            if (card.id == id) {
                Debug.Log("Remove card " + id);
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
