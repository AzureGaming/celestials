

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
            boardManager.AddToQueue(this);
        }
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

    public Summon GetSummon() {
        return GetComponentInChildren<Summon>();
    }

    void UpdateState(State state) {
        currentState = state;
    }

    void UpdateStatePrefab(State state) {
        if (type == TileType.Boss) {
            return;
        }

        if (state == State.Invalid) {
            SetIndicatorPrefab(invalidPrefab);
        } else if (state == State.Valid) {
            SetIndicatorPrefab(validPrefab);
        } else {
            SetIndicatorPrefab(neutralPrefab);
        }
    }

    void SetIndicatorPrefab(GameObject prefab) {
        foreach (Transform childTransform in transform) {
            if (childTransform.CompareTag("Indicator")) {
                Destroy(childTransform.gameObject);
            }
        }
        Instantiate(prefab, transform);
    }
}
