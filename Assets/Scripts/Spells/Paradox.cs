using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paradox : CardEffect {
    CardManager cardManager;
    Summoner summoner;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
        summoner = FindObjectOfType<Summoner>();
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(ParadoxRoutine());
    }

    IEnumerator ParadoxRoutine() {
        StartCoroutine(summoner.CastParadox());
        Card[] cards = cardManager.GetCardsInHand();
        foreach (Card card in cards) {
            card.SetManaCost(card.GetManaCost() - 1);
        }
        yield break;
    }
}
