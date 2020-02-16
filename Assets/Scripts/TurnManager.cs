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

    private void Awake() {
        player = FindObjectOfType<Player>();
        deck = FindObjectOfType<Deck>();
    }

    private void Start() {
        state = GameState.START;
    }

    public IEnumerator Initialize() {
        yield return StartCoroutine(deck.SetupDeck());
        yield return StartCoroutine(player.SetupPlayer());

        state = GameState.MULLIGAN;
        yield return StartCoroutine(player.StartMulligan());
        // initialize enemy
        // ...
        // start player turn
        StartCoroutine(StartPlayerTurn());
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
