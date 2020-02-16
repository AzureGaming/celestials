using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {
    int cardCount;
    int cardLimit;
    Text text;

    private void Awake() {
        text = GetComponentInChildren<Text>();
    }

    private void Start() {
        cardCount = 0;
        cardLimit = 30;
    }

    public IEnumerator SetupDeck() {
        FillDeck();
        yield break;
    }

    public void RemoveCard() {
        cardCount--;
        SetText(cardCount.ToString());
    }

    public void AddCard() {
        cardCount++;
        SetText(cardCount.ToString());
    }

    void FillDeck() {
        for (int i = 0; i < cardLimit; i++) {
            AddCard();
        }
    }

    void SetText(string value) {
        text.text = "Deck amount: " + value;
    }
}
