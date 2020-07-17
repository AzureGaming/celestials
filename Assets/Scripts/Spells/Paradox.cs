using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paradox : CardEffect {
    CardManager cardManager;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(ParadoxRoutine());
    }

    IEnumerator ParadoxRoutine() {
        Card[] cards = cardManager.GetCardsInHand();
        foreach (Card card in cards) {
            card.SetManaCost(card.GetManaCost() - 1);
        }
        yield break;
    }
}
