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
    public EffectIndicator attackIndicator;
    public TileType type;
    public int column;
    public int row;
    public enum State {
        Neutral,
        Invalid,
        Valid,
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

    public void SetAttackIndicator(bool value) {
        attackIndicator.SetIndicator(value);
    }

    public void SetActiveAttackIndicator(bool value) {
        attackIndicator.gameObject.SetActive(value);
    }

    public void UpdateIndicatorPosition() {
        attackIndicator.RefreshPosition();
    }

    public bool IsOccupied() {
        if (GetSummon() == null && GetBlockingCrystal() == null) {
            return false;
        }

        return true;
    }


    public Summon GetSummon() {
        return GetComponentInChildren<Summon>();
    }

    public BlockingCrystal GetBlockingCrystal() {
        return GetComponentInChildren<BlockingCrystal>();
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
