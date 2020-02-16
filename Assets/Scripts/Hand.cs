using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    Text text;
    int handCount;
    CardManager cardManager;
    HandSlot[] handSlots;

    private void Awake() {
        text = GetComponentInChildren<Text>();
        cardManager = FindObjectOfType<CardManager>();
        handSlots = GetComponentsInChildren<HandSlot>();
    }

    private void Start() {
        handCount = 0;
        foreach (HandSlot handSlot in handSlots) {
            handSlot.DestroyChildren();
        }
    }

    public void AddCard() {
        handCount++;

        GameObject card = cardManager.CreateCard();
        foreach (HandSlot handSlot in handSlots) {
            if (handSlot.transform.childCount < 1) {
                handSlot.InsertCard(card);
            }
        }


        text.text = "Hand Counter: " + handCount;
    }
}
