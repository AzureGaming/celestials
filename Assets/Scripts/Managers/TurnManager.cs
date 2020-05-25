
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
    BoardManager boardManager;
    CardManager cardManager;
    bool isMulliganConfirmed;

    private void Awake() {
        player = FindObjectOfType<Player>();
        boss = FindObjectOfType<Boss>();
        deck = FindObjectOfType<Deck>();
        board = FindObjectOfType<Board>();
        hand = FindObjectOfType<Hand>();
        gameManager = FindObjectOfType<GameManager>();
        boardManager = FindObjectOfType<BoardManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    private void Start() {
        state = GameState.START;
        isMulliganConfirmed = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            StartCoroutine(StartEnemyTurn());
        }
    }

    public IEnumerator Initialize() {
        //yield return StartCoroutine(deck.SetupDeck());
        yield return StartCoroutine(player.SetupPlayer());
        //yield return StartCoroutine(StartMulligan());
        //yield return StartCoroutine(boss.SetupBoss());
        //yield return StartCoroutine(ResolveSummonTurn());
        //yield return StartCoroutine(StartPlayerTurn());
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
        Debug.Log("End Mulligan phase");
        //foreach (System.Guid id in gameManager.mulligans) {
        //    yield return StartCoroutine(player.ReturnCard(id));
        //}

        foreach (System.Guid id in gameManager.mulligans) {
            yield return StartCoroutine(player.DrawCard());
        }

        yield break;
    }

    IEnumerator ResolveSummonTurn() {
        //Summon[] summonsOnBoard = board.GetSummons();
        //Summon[] summons = new Summon[summonsOnBoard.Length];
        //System.Array.Copy(summonsOnBoard, summons, summons.Length);

        //System.Array.Sort(summons, (x, y) => x.GetOrder() - y.GetOrder());
        //foreach (Summon summon in summons) {
        //    yield return StartCoroutine(summon.ExecuteAction());
        //    if (boss.getHealth() < 1) {
        //        state = GameState.WIN;
        //        Debug.Log("Implement win scenario");
        //        yield break;
        //    }
        //}

        yield return StartCoroutine(boardManager.ResolveStagesRoutine());
    }

    IEnumerator StartPlayerTurn() {
        Debug.Log("Fill hand");
        while (cardManager.GetCardsInHand().Length < 5) {
            yield return StartCoroutine(player.DrawCard());

            //if (deck.GetCardsInDeck() < 1) {
            //    deck.Reload();
            //}
        }
        Debug.Log("Refresh mana");
        yield return StartCoroutine(player.GainMaxMana(1));
        yield return StartCoroutine(player.RefreshMana());

        Debug.Log("Player agency starts");
        state = GameState.PLAYERTURN;
        //player.SetIsTurnDone(false);
        //board.EnablePlay();
        //yield return new WaitUntil(() => player.GetIsTurnDone());
        //StartCoroutine(StartEnemyTurn());
        //yield break;
    }

    IEnumerator StartEnemyTurn() {
        Debug.Log("Start enemy turn");
        state = GameState.ENEMYTURN;
        //board.DisablePlay();
        yield return StartCoroutine(boss.RunTurnRoutine());
        if (player.GetHealth() < 1) {
            state = GameState.LOSE;
            Debug.Log("Implement Lose scenario");
            yield break;
        }
        //StartCoroutine(StartPlayerTurn());
        yield break;
    }
}
