﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour {
    List<Card> cards = new List<Card>();

    public void AddCard(Card card) {
        cards.Add(card);
    }
}
