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
    }

    private void Start() {
        state = GameState.START;
        isMulliganConfirmed = false;
    }

    public IEnumerator Initialize() {
        yield return StartCoroutine(deck.SetupDeck());
        yield return StartCoroutine(player.SetupPlayer());
        yield return StartCoroutine(StartMulligan());
        yield return StartCoroutine(boss.SetupBoss());
        StartCoroutine(StartPlayerTurn());
        yield break;
    }

    public void SetMulliganConfirmed(bool value) {
        isMulliganConfirmed = value;
    }

    IEnumerator StartMulligan() {
        Debug.Log("Start Mulligan");
        state = GameState.MULLIGAN;
        SetMulliganConfirmed(false);
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
        Debug.Log("Summons action");
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

        Debug.Log("Fill hand");
        while (player.GetHand().handCardIds.Count < 5) {
            yield return StartCoroutine(player.DrawCard());

            if (deck.GetCardsInDeck() < 1) {
                deck.Reload();
            }
        }
        Debug.Log("Refresh mana");
        yield return StartCoroutine(player.GainMaxMana(1));
        yield return StartCoroutine(player.RefreshMana());

        Debug.Log("Player agency starts");
        state = GameState.PLAYERTURN;
        player.SetIsTurnDone(false);
        board.EnablePlay();
        yield return new WaitUntil(() => player.GetIsTurnDone());
        StartCoroutine(StartEnemyTurn());
        yield break;
    }

    IEnumerator StartEnemyTurn() {
        Debug.Log("Start enemy turn");
        state = GameState.ENEMYTURN;
        board.DisablePlay();
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
