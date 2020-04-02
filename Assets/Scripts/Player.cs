using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    GameManager gameManager;
    TurnManager turnManager;
    UIManager uiManager;
    Deck deck;
    Hand hand;
    int handLimit;
    int health;
    bool isTurnDone;
    int mana;
    int maxMana;
    int numberOfCardsHeld;

    private void Awake() {
        deck = GetComponentInChildren<Deck>();
        hand = GetComponentInChildren<Hand>();
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start() {
        handLimit = 5;
        health = 30;
        isTurnDone = false;
        mana = 0;
        maxMana = 0;
        numberOfCardsHeld = hand.transform.childCount;
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
        Debug.Log("Set up player " + numberOfCardsHeld);    
        for (int i = numberOfCardsHeld; i < handLimit; i++) {
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

    public IEnumerator ReturnCard(System.Guid id) {
        hand.RemoveCard(id);
        deck.AddCard();
        yield break;
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
