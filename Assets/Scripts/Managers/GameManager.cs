﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<System.Guid> mulligans;
    TurnManager turnManager;
    UIManager uiManager;
    int entityIdCounter = 0;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
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
    }

    public int GetNextEntityId() {
        int nextEntityId = entityIdCounter;
        Debug.Log("next id" + nextEntityId);
        entityIdCounter++;
        return nextEntityId;
    }

    void SetupGame() {
        uiManager.SetLocationSelectionPrompt(false);
        StartCoroutine(turnManager.Initialize());
    }
}
