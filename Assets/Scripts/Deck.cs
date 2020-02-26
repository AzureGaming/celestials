using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {
    int cardsInDeck;
    int deckLimit;
    Text text;

    private void Awake() {
        text = GetComponentInChildren<Text>();
    }

    private void Start() {
        cardsInDeck = 0;
        deckLimit = 30;
    }

    public int GetCardsInDeck() {
        return cardsInDeck;
    }

    public IEnumerator SetupDeck() {
        FillDeck();
        yield break;
    }

    public void RemoveCard() {
        cardsInDeck--;
        SetText(cardsInDeck.ToString());
    }

    public void AddCard() {
        cardsInDeck++;
        SetText(cardsInDeck.ToString());
    }

    public IEnumerator Reload() {
        FillDeck();
        yield break;
    }

    void FillDeck() {
        for (int i = 0; i < deckLimit; i++) {
            AddCard();
        }
    }

    void SetText(string value) {
        text.text = "Deck amount: " + value;
    }
}
