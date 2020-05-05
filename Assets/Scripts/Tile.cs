using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour {
    [SerializeField] GameObject validPrefab;
    [SerializeField] GameObject invalidPrefab;
    [SerializeField] GameObject neutralPrefab;
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
            UpdatePrefab(value);
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

    Summon GetSummon() {
        return GetComponent<Summon>();
    }

    //public void Summon(Card card) {
    //    foreach (Transform child in transform) {
    //        if (child.CompareTag("Summon")) {
    //            Debug.LogWarning("Overwriting summon...");
    //        }
    //    }

        //GameObject summonObj = Instantiate(card.summonPrefab, transform);
        //Summon summon = summonObj.GetComponent<Summon>();
        //summon.InitSummon(card, boardManager.GetCardOrder());
        //boardManager.IncrementCardOrder(1);
    //}

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
