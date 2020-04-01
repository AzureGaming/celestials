using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] GameObject validPrefab;
    [SerializeField] GameObject invalidPrefab;
    [SerializeField] GameObject neutralPrefab;
    public enum State {
        Neutral,
        Invalid,
        Valid
    }
    Card card;

    State _currentState;
    State currentState {
        get {
            return _currentState;
        }
        set {
            _currentState = value;
            UpdatePrefab(value);
        }
    }

    public void Summon(Card card) {
        if (this.card) {
            Debug.LogWarning("Tile is overwriting card" + card.name);
        }
        this.card = card;
        Instantiate(card.prefab, transform);
    }

    public void UpdateStatus() {
        bool isEmptyTile = true;
        foreach (Transform child in transform) {
            if (child.CompareTag("Summon")) {
                UpdateState(State.Invalid);
                isEmptyTile = false;
            }
        }

        if (isEmptyTile) {
            UpdateState(State.Valid);
        }
    }

    void UpdateState(State state) {
        currentState = state;
    }

    void UpdatePrefab(State state) {
        if (state == State.Invalid) {
            foreach (Transform child in transform) {
                if (child.CompareTag("Indicator")) {
                    Destroy(child.gameObject);
                    Instantiate(invalidPrefab, transform);
                    break;
                }
            }
        } else if (state == State.Valid) {
            foreach (Transform child in transform) {
                if (child.CompareTag("Indicator")) {
                    Destroy(child.gameObject);
                    Instantiate(validPrefab, transform);
                    break;
                }
            }
        } else {
            foreach (Transform child in transform) {
                if (child.CompareTag("Indicator")) {
                    Destroy(child.gameObject);
                    Instantiate(neutralPrefab, transform);
                    break;
                }
            }
        }
    }
}
