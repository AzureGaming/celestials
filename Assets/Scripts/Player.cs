using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Deck deck;
    Hand hand;
    int handLimit;
    int health;

    private void Awake() {
        deck = GetComponentInChildren<Deck>();
        hand = GetComponentInChildren<Hand>();
    }

    private void Start() {
        handLimit = 5;
        health = 30;
    }

    public IEnumerator SetupPlayer() {
        for (int i = 0; i < handLimit; i++) {
            yield return StartCoroutine(DrawCard());
        }
        yield break;
    }

    public IEnumerator DrawCard() {
        deck.RemoveCard();
        hand.AddCard();
        yield return null;
    }

    public IEnumerator StartMulligan() {
        yield return StartCoroutine(GetMulliganChoices());
        yield break;
    }

    IEnumerator GetMulliganChoices() {
        yield break;
    }
}
