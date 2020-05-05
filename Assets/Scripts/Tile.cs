using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TileType {
    Summon,
    Boss
}
public class Tile : MonoBehaviour {
    [SerializeField] GameObject validPrefab;
    [SerializeField] GameObject invalidPrefab;
    [SerializeField] GameObject neutralPrefab;
    public TileType type;
    public int column;
    public int row;
    public enum State {
        Neutral,
        Invalid,
        Valid
    }
    GameManager gameManager;
    BoardManager boardManager;

    State _currentState;
    State currentState {
        get {
            return _currentState;
        }
        set {
            _currentState = value;
            UpdateStatePrefab(value);
        }
    }

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    private void OnMouseDown() {
        if (currentState == State.Valid) {
            Debug.Log("Add");
            boardManager.AddToQueue(this);
        }
    }

    Summon GetSummon() {
        return GetComponent<Summon>();
    }

    public void SetValidState() {
        UpdateState(State.Valid);
    }

    public void SetInvalidState() {
        UpdateState(State.Invalid);
    }

    public void SetNeutralState() {
        UpdateState(State.Neutral);
    }

    public bool CheckOccupied() {
        if (GetComponentInChildren<Summon>()) {
            return true;
        }

        return false;
    }

    void UpdateState(State state) {
        currentState = state;
    }

    void UpdateStatePrefab(State state) {
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
