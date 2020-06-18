
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    START, PLAYERTURN, ENEMYTURN, WIN, LOSE
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
    bool waitForPlayer = false;

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
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            StartCoroutine(ResolveSummonTurn());

        }
    }

    public IEnumerator Initialize() {
        yield return StartCoroutine(player.SetupPlayer());
        yield return StartCoroutine(StartPlayerTurn());
    }

    public void SetWaitForPlayer(bool shouldWait) {
        waitForPlayer = shouldWait;
    }

    IEnumerator ResolveSummonTurn() {
        Debug.Log("Summon turn");
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
        yield return StartCoroutine(StartPlayerTurn());
    }

    IEnumerator StartPlayerTurn() {
        state = GameState.PLAYERTURN;
        while (cardManager.GetCardsInHand().Length < 5) {
            yield return StartCoroutine(cardManager.DrawToHand());
        }
        yield return StartCoroutine(player.GainMaxMana(1));
        yield return StartCoroutine(player.RefreshMana());

        SetWaitForPlayer(true);
        yield return new WaitUntil(() => !waitForPlayer);
        yield return StartCoroutine(StartEnemyTurn());
    }

    IEnumerator StartEnemyTurn() {
        Debug.Log("Start enemy turn");
        state = GameState.ENEMYTURN;
        yield return StartCoroutine(boss.RunTurnRoutine());
        if (player.GetHealth() < 1) {
            state = GameState.LOSE;
            Debug.Log("Implement Lose scenario");
            yield break;
        }
        yield return StartCoroutine(ResolveSummonTurn());
    }
}
