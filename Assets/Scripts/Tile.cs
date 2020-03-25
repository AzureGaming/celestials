using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public Card card;

    public void Summon(Card card) {
        this.card = card;
        Instantiate(card.prefab, transform);
    }
}
