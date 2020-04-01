﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<System.Guid> mulligans;
    public Card cardToSummon;

    TurnManager turnManager;
    Text mulliganIds;
    UIManager uiManager;
    BoardManager boardManager;
    Player player;

    private void Awake() {
        player = FindObjectOfType<Player>();
        turnManager = FindObjectOfType<TurnManager>();
        mulliganIds = GameObject.Find("MulliganIds").GetComponent<Text>();
        uiManager = FindObjectOfType<UIManager>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    private void Start() {
        mulligans = new List<System.Guid>(2);
        SetupGame();
    }

    public void SetMulligan(System.Guid cardId) {
        if (mulligans.Contains(cardId)) {
            Debug.Log("Remove " + cardId + " from mulligan");
            mulligans.Remove(cardId);
        } else if (mulligans.Count < 2) {
            Debug.Log("Add" + cardId + " to mulligan");
            mulligans.Add(cardId);
        }
        UpdateIdsDisplay();
    }

    public IEnumerator StartCardSummon(Card card) {
        cardToSummon = card;
        player.LoseMana(card.manaCost);
        uiManager.SetLocationSelectionPrompt(true);
        boardManager.DetectTileState();
        yield break;
    }

    void SetupGame() {
        uiManager.SetLocationSelectionPrompt(false);
        StartCoroutine(turnManager.Initialize());
    }

    void UpdateIdsDisplay() {
        mulliganIds.text = "";
        foreach (System.Guid id in mulligans) {
            mulliganIds.text += id.ToString() + "\n";
        }
    }
}
