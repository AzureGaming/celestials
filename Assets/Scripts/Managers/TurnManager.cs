
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    START, PLAYERTURN, ENEMYTURN, WIN, LOSE
}

public class TurnManager : MonoBehaviour {
    public event EventHandler OnPlayerTurnStart;
    public event EventHandler OnPlayerTurnEnd;
    public GameState state;
    public AttackQueueManager queueManager;
    Player player;
    Boss boss;
    Deck deck;
    Board board;
    Hand hand;
    GameManager gameManager;
    BoardManager boardManager;
    CardManager cardManager;
    UIManager uiManager;
    Summoner summoner;
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
        uiManager = FindObjectOfType<UIManager>();
        summoner = FindObjectOfType<Summoner>();
    }

    private void Start() {
        state = GameState.START;
    }

    public IEnumerator Initialize() {
        yield return StartCoroutine(player.SetupPlayer());
        boss.Initialize();
        yield return StartCoroutine(StartPlayerTurn());
    }

    public void SetWaitForPlayer(bool shouldWait) {
        waitForPlayer = shouldWait;
    }

    IEnumerator ResolveSummonTurn() {
        yield return StartCoroutine(boardManager.ResolveStagesRoutine());
        queueManager.RefreshIndicators(true);

        if (boss.getHealth() < 1) {
            state = GameState.WIN;
            uiManager.SetWinScreen(true);
            yield break;
        }

        yield return StartCoroutine(StartPlayerTurn());
    }

    IEnumerator StartPlayerTurn() {
        state = GameState.PLAYERTURN;
        OnPlayerTurnStart?.Invoke(this, EventArgs.Empty);
        while (cardManager.GetCardsInHand().Length < 5) {
            yield return StartCoroutine(cardManager.DrawToHand());
        }
        yield return StartCoroutine(player.RefreshMana());

        SetWaitForPlayer(true);
        Card[] cards = hand.GetCards();
        foreach (Card card in cards) {
            card.GetComponentInChildren<UIHoverSize>().enabled = true;
        }
        yield return new WaitUntil(() => !waitForPlayer);
        foreach (Card card in hand.GetCards()) {
            card.GetComponent<UIHoverSize>().enabled = false;
        }
        OnPlayerTurnEnd?.Invoke(this, EventArgs.Empty);
        yield return StartCoroutine(StartEnemyTurn());
    }

    IEnumerator StartEnemyTurn() {
        state = GameState.ENEMYTURN;
        yield return StartCoroutine(boss.RunTurnRoutine());
        Debug.Log("Hello" + summoner.GetHealth());
        if (summoner.GetHealth() < 1) {
            state = GameState.LOSE;
            uiManager.SetLoseScreen(true);
            yield break;
        }
        yield return StartCoroutine(ResolveSummonTurn());
    }
}
