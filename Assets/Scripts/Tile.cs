using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    Card card;

    public void Summon(Card card) {
        if (this.card) {
            Debug.LogWarning("Tile is overwriting card" + card.name);
        }
        this.card = card;
        Instantiate(card.prefab, transform);
    }
}
