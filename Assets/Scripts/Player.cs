﻿using System.Collections;
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
    int mana;
    int maxMana;

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
        mana = 0;
        maxMana = 0;
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
        yield return StartCoroutine(GainMana(1));
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

    public IEnumerator GainMana(int amount) {
        mana += amount;
        if (mana > maxMana) {
            mana = maxMana;
        }
        yield break;
    }

    public IEnumerator LoseMana(int amount) {
        mana -= amount;
        if (mana < 0) {
            mana = 0;
        }
        yield break;
    }

    public IEnumerator GainMaxMana(int amount) {
        maxMana += amount;
        yield break;
    }

    public IEnumerator LoseMaxMana(int amount) {
        maxMana -= amount;
        yield break;
    }

    public IEnumerator RefreshMana() {
        mana = maxMana;
        yield break;
    }
}
