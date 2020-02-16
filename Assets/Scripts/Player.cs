using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    GameManager gameManager;
    Deck deck;
    Hand hand;
    int handLimit;
    int health;

    private void Awake() {
        deck = GetComponentInChildren<Deck>();
        hand = GetComponentInChildren<Hand>();
        gameManager = FindObjectOfType<GameManager>();
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
        hand.DrawCard();
        yield return null;
    }

    public IEnumerator ReturnCard(int id) {
        hand.RemoveCard(id);
        deck.AddCard();
        yield break;
    }
}
