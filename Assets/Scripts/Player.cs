using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    GameManager gameManager;
    TurnManager turnManager;
    Deck deck;
    Hand hand;
    int handLimit;
    int health;
    bool isTurnDone;

    private void Awake() {
        deck = GetComponentInChildren<Deck>();
        hand = GetComponentInChildren<Hand>();
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void Start() {
        handLimit = 5;
        health = 30;
        isTurnDone = false;
    }

    public Hand GetHand() {
        return hand;
    }

    public bool GetIsTurnDone() {
        return isTurnDone;
    }

    public int GetHealth() {
        return health;
    }

    public void SetIsTurnDone(bool value) {
        isTurnDone = value;
    }

    public IEnumerator SetupPlayer() {
        for (int i = 0; i < handLimit; i++) {
            yield return StartCoroutine(DrawCard());
        }
        yield break;
    }

    public IEnumerator DrawCard() {
        deck.RemoveCard();
        StartCoroutine(hand.DrawCard());
        yield break;
    }

    public IEnumerator ReturnCard(int id) {
        hand.RemoveCard(id);
        deck.AddCard();
        yield break;
    }
}
