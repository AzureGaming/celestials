using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    START, MULLIGAN, PLAYERTURN, ENEMYTURN, WIN, LOSE
}

public class TurnManager : MonoBehaviour {
    public GameState state;
    Player player;
    Boss boss;
    Deck deck;
    Board board;
    Hand hand;
    GameManager gameManager;
    bool isMulliganConfirmed;

    private void Awake() {
        player = FindObjectOfType<Player>();
        boss = FindObjectOfType<Boss>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
        hand = FindObjectOfType<Hand>();
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
        yield return StartCoroutine(boss.SetupBoss());
        StartCoroutine(StartPlayerTurn());
        yield break;
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
        Summon[] summons = board.GetSummons();
        System.Array.Sort(summons, (x, y) => x.getOrder() - y.getOrder());
        foreach (Summon summon in summons) {
            yield return StartCoroutine(summon.ExecuteAction());
            if (boss.getHealth() < 1) {
                state = GameState.WIN;
                Debug.Log("Implement win scenario");
                yield break;
            }
        }

        while (player.GetHand().handCardIds.Count < 5) {
            yield return StartCoroutine(player.DrawCard());
        }

        // if no more cards 
        // shuffle discarded cards
        // draw cards to 5

        // refresh mana to cap
        // activate player hand UI
        yield return new WaitUntil(() => player.GetIsTurnDone());
        StartCoroutine(StartEnemyTurn());
        yield break;
    }

    IEnumerator StartEnemyTurn() {
        state = GameState.ENEMYTURN;
        yield return StartCoroutine(boss.RunTurnRoutine());
        if (player.GetHealth() < 1) {
            state = GameState.LOSE;
            Debug.Log("Implement Lose scenario");
            yield break;
        }
        StartCoroutine(StartPlayerTurn());
        yield break;
    }
}
