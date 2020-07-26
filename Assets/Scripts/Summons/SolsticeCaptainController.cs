﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SolsticeCaptainController : SummonController {
    Hand hand;
    int HOWL_DAMAGE = 2;

    private void Awake() {
        hand = FindObjectOfType<Hand>();
    }

    protected override IEnumerator Howl() {
        // give random celestial in hand +2 damage
        Card[] cards = hand.GetCards();
        bool isValid = cards.Any(card => card.GetType() == CardType.Summon);
        if (!isValid) {
            yield break;
        }
        Card randomCard = null;
        while (randomCard == null) {
            Card pickedCard = cards[Random.Range(0, cards.Length)];
            if (pickedCard.GetType() == CardType.Summon) {
                randomCard = pickedCard;
                break;
            }
        }
        randomCard.AddAttack(HOWL_DAMAGE);
    }
}
