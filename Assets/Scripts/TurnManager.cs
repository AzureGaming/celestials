using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    START, MULLIGAN, PLAYERTURN, ENEMYTURN, WIN, LOSE
}

public class TurnManager : MonoBehaviour {
    public GameState state;
    Player player;
    Deck deck;
    GameManager gameManager;
    bool isMulliganConfirmed;

    private void Awake() {
        player = FindObjectOfType<Player>();
        deck = FindObjectOfType<Deck>();
        gameManager = FindObjectOfType<GameManager>();
        isMulliganConfirmed = false;
    }

    private void Start() {
        state = GameState.START;
    }

    public IEnumerator Initialize() {
        yield return StartCoroutine(deck.SetupDeck());
        yield return StartCoroutine(player.SetupPlayer());
        yield return StartCoroutine(StartMulligan());
        // initialize enemy
        // ...
        // start player turn
        StartCoroutine(StartPlayerTurn());
    }

    public void ConfirmMulligan() {
        isMulliganConfirmed = true;
    }

    IEnumerator StartMulligan() {
        Debug.Log("Start Mulligan");
        state = GameState.MULLIGAN;
        yield return new WaitUntil(() => isMulliganConfirmed);
        foreach (int id in gameManager.mulligans) {
            yield return StartCoroutine(player.ReturnCard(id));
        }

        foreach (int id in gameManager.mulligans) {
            yield return StartCoroutine(player.DrawCard());
        }
        yield break;
    }

    IEnumerator StartPlayerTurn() {
        state = GameState.PLAYERTURN;


        // Resolve pending summon actions
        // if boss dies
        // set game state to win
        // if no more cards 
        // shuffle discarded cards
        // draw cards to 5
        // refresh mana to cap
        // activate player hand UI
        // Receive player input
        // if end turn button is clicked 
        // set game state to enemey turn
        yield return null;
    }

    void StartEnemyTurn() {
        state = GameState.ENEMYTURN;

        // Delegate to enemy AI
        // If player dies
        // set game state to lose
        // set game state to player turn
    }
}
