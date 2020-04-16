using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour {
    [SerializeField] GameObject validPrefab;
    [SerializeField] GameObject invalidPrefab;
    [SerializeField] GameObject neutralPrefab;
    public UnityEvent summoned;
    public int column;
    public int row;
    public enum State {
        Neutral,
        Invalid,
        Valid
    }
    Card card;
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
        Debug.Log("On mouse down tile");

        if (currentState == State.Valid) {
            Summon(gameManager.cardToSummon);
            summoned.Invoke();
            // clear card to summon
        }
    }

    void Summon(Card card) {
        foreach (Transform child in transform) {
            if (child.CompareTag("Summon")) {
                Debug.LogWarning("Overwriting summon...");
            }
        }

        GameObject summonObj = Instantiate(card.prefab, transform);
        Summon summon = summonObj.GetComponent<Summon>();
        summon.InitSummon(card, boardManager.GetCardOrder());
        boardManager.IncrementCardOrder(1);
    }

    public void SetSelectState() {
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

    public void SetNeutralState() {
        UpdateState(State.Neutral);
    }

    public IEnumerator MoveSummon(Summon summon) {
        yield return StartCoroutine(boardManager.MoveSummonFromTile(summon, this));
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
