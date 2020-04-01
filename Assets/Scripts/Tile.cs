using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] GameObject validPrefab;
    [SerializeField] GameObject invalidPrefab;
    [SerializeField] GameObject neutralPrefab;
    Card card;
    State currentState;
    public enum State {
        Neutral,
        InvalidSelection,
        ValidSelection
    }

    public void Summon(Card card) {
        if (this.card) {
            Debug.LogWarning("Tile is overwriting card" + card.name);
        }
        this.card = card;
        Instantiate(card.prefab, transform);
    }

    public void UpdateState(State state) {
        currentState = state;
    }
}
