using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<int> cardsInhand;
    GameManager gameManager;
    TurnManager turnManager;
    CardManager cardManager;
    UIManager uiManager;
    Deck deck;
    Hand hand;
    int handLimit;
    int health;
    int mana;
    int maxMana;
    int numberOfCardsHeld;

    private void Awake() {
        deck = GetComponentInChildren<Deck>();
        hand = GetComponentInChildren<Hand>();
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    private void Start() {
        handLimit = 5;
        health = 30;
        mana = 0;
        maxMana = 0;
        numberOfCardsHeld = hand.transform.childCount;
    }

    public int GetHealth() {
        return health;
    }

    public IEnumerator SetupPlayer() {
        Debug.Log("Set up player " + numberOfCardsHeld);
        for (int i = numberOfCardsHeld; i < handLimit; i++) {
            yield return StartCoroutine(cardManager.DrawToHand());
        }
        yield return StartCoroutine(GainMaxMana(1));
        yield return StartCoroutine(GainMana(1));
        yield break;
    }

    public int GetMana() {
        return mana;
    }

    public IEnumerator GainMana(int amount) {
        mana += amount;
        if (mana > maxMana) {
            mana = maxMana;
        }
        uiManager.SetCurrentMana(mana);
        yield break;
    }

    public IEnumerator LoseMana(int amount) {
        mana -= amount;
        if (mana < 0) {
            mana = 0;
        }
        uiManager.SetCurrentMana(mana);
        yield break;
    }

    public IEnumerator GainMaxMana(int amount) {
        maxMana += amount;
        uiManager.SetMaxMana(maxMana);
        yield break;
    }

    public IEnumerator LoseMaxMana(int amount) {
        maxMana -= amount;
        uiManager.SetMaxMana(maxMana);
        yield break;
    }

    public IEnumerator RefreshMana() {
        mana = maxMana;
        uiManager.SetCurrentMana(mana);
        yield break;
    }
}
